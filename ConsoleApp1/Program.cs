using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

#region main

InlineKeyboardMarkup baseInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Про бота ℹ️", callbackData: "Про бота"),
            InlineKeyboardButton.WithCallbackData(text:  "Курсова робота 📒", callbackData:  "Course menu"),
        },
        // second row
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Налаштування ⚙️", callbackData: "Налаштування"),
        },
    });

InlineKeyboardMarkup courseWorkInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Про курсову ℹ️", callbackData: "Про курсову"),
            InlineKeyboardButton.WithCallbackData(text:  "Класи 📒", callbackData:  "Класи"),
        },
        // second row
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Демонстрація 💡", callbackData: "Демонстрація"),
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "Головна"),
        },
    });

InlineKeyboardMarkup clasessInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Структура 🗂", callbackData: "Структура"),
            InlineKeyboardButton.WithCallbackData(text: "Діаграма 📊", callbackData: "Діаграма"),
        },
        // second row
        new []
        {
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "Course menu"),
        },
    });

InlineKeyboardMarkup demoInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Код 🖥", callbackData: "Код"),
            InlineKeyboardButton.WithCallbackData(text: "Скріни 🖼", callbackData:  "Скріни"),
        },
        // second row
        new []
        {
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "Course menu"),
        },
    });

var botClient = new TelegramBotClient("6569703281:AAFX7AovCc58ZKunG__NPDfukXLLTeiF564");

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username} FirstName:{me.FirstName}, Id:{me.Id}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

#endregion

async Task<Message> MesageReplyer(Update update, CancellationToken cancellationToken)
{
    if (update.CallbackQuery.Data == "Про бота")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Цей бот був створений для опису функціоналу курсового проєкта. В описі бота є посилання на GitHub репозиторій проєкта.\nПосилання на GitHub репозиторій цього телеграм-бота також є у описі бота",
        disableNotification: true,
         replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Course menu")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Що саме ви хочете дізнатися?",
        disableNotification: true,
         replyMarkup: courseWorkInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Налаштування")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Немає їх",
        disableNotification: true,
         replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Про курсову")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Всю діаграму можно умовно розділити на “користувачі” та “курси”.\r\n\r\nГоловний класс у розділі “користувачі” - абстрактний класс User (користувач): він реалізує вбудований інтерфейс IComparble. \r\nВід абстрактного класу наслідуються чотири класси: UnAuthorised, Authorised, Teacher та Admin.\r\nUnAuthorised - найпростіший, використовується перед тим, як користувач залогиниться/авторизується. Не має якогось явного функціоналу.\r\nAuthorised - звичайний користувач. Може покупати курси та проходити їх. Реалізує інтерфейс IStudyable.\r\nTeacher - вчитель. Відрізняється від звичайного користувача тим, що не може проходити курси. Реалізує інтерфейс ITeacheble.\r\nAdmin - адміністратор. Також може створювати курси, але має більш обширний функціонал: може дивитися статистику курсів та створювати користувачів. Реалізує інтерфейс ITeacheble.\r\n\r\nУ розділі курси основний класс - Course (курс). Він має багато характеристик, одна з яких множина класу Module (модуль). У свою чергу один модуль містить у собі множину класу Lesson (урок, заняття).\r\n\r\nЦі два розділи об’єднує у собі класс CoursesApp (клас застосунку).\r\n\r\nПоміж класів є інтерфейси IStudyable та ITeacheble:\r\nIStudyable містить функціонал навчання.\r\nITeacheble містить функціонал створення, редагування та моніторинг створених курсів\r\n",
        disableNotification: true,
         replyMarkup: courseWorkInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Класи")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Що саме ви хочете дізнатися?",
        disableNotification: true,
        replyMarkup: clasessInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Демонстрація")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "d",
        disableNotification: true,
        replyMarkup: demoInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Головна")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text:"Головна сторінка",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Структура")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Всю діаграму можно умовно розділити на “користувачі” та “курси”.\r\n\r\nГоловний класс у розділі “користувачі” - абстрактний класс User (користувач): він реалізує вбудований інтерфейс IComparble. \r\nВід абстрактного класу наслідуються чотири класси: UnAuthorised, Authorised, Teacher та Admin.\r\nUnAuthorised - найпростіший, використовується перед тим, як користувач залогиниться/авторизується. Не має якогось явного функціоналу.\r\nAuthorised - звичайний користувач. Може покупати курси та проходити їх. Реалізує інтерфейс IStudyable.\r\nTeacher - вчитель. Відрізняється від звичайного користувача тим, що не може проходити курси. Реалізує інтерфейс ITeacheble.\r\nAdmin - адміністратор. Також може створювати курси, але має більш обширний функціонал: може дивитися статистику курсів та створювати користувачів. Реалізує інтерфейс ITeacheble.\r\n\r\nУ розділі курси основний класс - Course (курс). Він має багато характеристик, одна з яких множина класу Module (модуль). У свою чергу один модуль містить у собі множину класу Lesson (урок, заняття).\r\n\r\nЦі два розділи об’єднує у собі класс CoursesApp (клас застосунку).\r\n\r\nПоміж класів є інтерфейси IStudyable та ITeacheble:\r\nIStudyable містить функціонал навчання.\r\nITeacheble містить функціонал створення, редагування та моніторинг створених курсів\r\n",
        disableNotification: true,
        replyMarkup: clasessInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Діаграма")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Діаграмма класів",
        disableNotification: true,
        replyMarkup: new InlineKeyboardMarkup(
            InlineKeyboardButton.WithUrl(
            text:"Lucid.app (потрібно зареєструватися на сайті)." ,
            url:"https://lucid.app/lucidchart/c216c932-ed99-4d75-971e-953ba558b242/edit?viewport_loc=-134%2C356%2C2616%2C1200%2C0_0&invitationId=inv_6c4f668c-c05a-4054-85f0-1fe9c4167e4c")),
        cancellationToken: cancellationToken);
    }


    return await botClient.SendTextMessageAsync(
    chatId: update.CallbackQuery.From.Id,
    text: "replyText",
    disableNotification: true,
    cancellationToken: cancellationToken);
}
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    string replyText = "";


    if (update.Type == UpdateType.CallbackQuery)
    {
        var replMessage = MesageReplyer(update, cancellationToken).Result;

        Console.WriteLine($"replyed: {replMessage.Text}");
        return;
    }

    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    //Only process text messages
    if (message.Text is not { } messageText)
        return;
    //var message = update.Message;
    //var messageText = message?.Text;

    var chatId = message.Chat.Id;

    replyText = messageText;

    if (messageText == "/start")
    {
        await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Вітаю, цей бот був створений для опису функціоналу курсового проєкта. В описі бота є посилання на GitHub репозиторій проєкта.\nПосилання на GitHub репозиторій цього телеграм-бота також є у описі бота ",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }
    else
    {
        message = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Не вірна команда",
        replyToMessageId: update?.Message?.MessageId,
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        //replyMarkup: new InlineKeyboardMarkup(
        //    InlineKeyboardButton.WithUrl(
        //    text: $"https://lucid.app/lucidchart/c216c932-ed99-4d75-971e-953ba558b242/edit?viewport_loc=-134%2C356%2C2616%2C1200%2C0_0&invitationId=inv_6c4f668c-c05a-4054-85f0-1fe9c4167e4c",
        //     url:"",
        cancellationToken: cancellationToken);
    }

    ConsoleLogging(message);
}

void ConsoleLogging(Message message)
{
    message.Date.ToLocalTime();

    Console.Write(
      $"{message.From?.FirstName} sent message {message.MessageId} : {message.Text} " +
      $"to chat {message.Chat.Id} at ");

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"{message.Date}. ");

    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write(
      $"It is a reply to message {message.ReplyToMessage?.MessageId} : {message.ReplyToMessage?.Text} " +
      $"and has {message.Entities?.Length} message entities.\n");
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}