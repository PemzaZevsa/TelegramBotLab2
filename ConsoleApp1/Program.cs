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
            InlineKeyboardButton.WithCallbackData(text: "Про бота ℹ️", callbackData: "AboutBot"),
            InlineKeyboardButton.WithCallbackData(text:  "Курсова робота 📒", callbackData:  "CourseMenu"),
        },
        //new []
        //{
        //    InlineKeyboardButton.WithCallbackData(text: "Налаштування ⚙️", callbackData: "Налаштування"),
        //},
    });

InlineKeyboardMarkup courseWorkInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Про курсову ℹ️", callbackData: "AboutCourseWork"),
            InlineKeyboardButton.WithCallbackData(text:  "Класи 📒", callbackData:  "ClassesMenu"),
        },
        // second row
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Демонстрація 💡", callbackData: "DemoMenu"),
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "MainMenu"),
        },
    });

InlineKeyboardMarkup clasessInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Структура 🗂", callbackData: "Structure"),
            InlineKeyboardButton.WithCallbackData(text: "Діаграма 📊", callbackData: "Diagram"),
        },
        // second row
        new []
        {
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "CourseMenu"),
        },
    });

InlineKeyboardMarkup demoInlineKeyboard = new(new[]
{
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Код 🖥", callbackData: "Code"),
            InlineKeyboardButton.WithCallbackData(text: "Скріни 🖼", callbackData:  "Screens"),
        },
        // second row
        new []
        {
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "CourseMenu"),
        },
    });

InlineKeyboardMarkup diagramInlineKeyboard = new InlineKeyboardMarkup(new[] {
        new[]
        {
            InlineKeyboardButton.WithUrl(
            text: "Lucid.app (потрібно зареєструватися на сайті).",
         url: "https://lucid.app/lucidchart/c216c932-ed99-4d75-971e-953ba558b242/edit?viewport_loc=-134%2C356%2C2616%2C1200%2C0_0&invitationId=inv_6c4f668c-c05a-4054-85f0-1fe9c4167e4c"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Структура 🗂", callbackData: "Structure"),
            InlineKeyboardButton.WithCallbackData(text: "Діаграма 📊", callbackData: "Diagram"),
        },
        // second row
        new[]
        {
             InlineKeyboardButton.WithCallbackData(text: "Повернутися ➡️", callbackData: "CourseMenu"),
        },

            });

#region APItoken
var botClient = new TelegramBotClient("6569703281:AAFX7AovCc58ZKunG__NPDfukXLLTeiF564");
#endregion

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
    if (update.CallbackQuery.Data == "AboutBot")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Цей бот був створений для опису функціоналу курсового проєкта. В описі бота є посилання на GitHub репозиторій проєкта.\nПосилання на GitHub репозиторій цього телеграм-бота також є у описі бота",
        disableNotification: true,
         replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "CourseMenu")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Що саме ви хочете дізнатися?",
        disableNotification: true,
         replyMarkup: courseWorkInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "AboutCourseWork")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Тема курсової роботи: \"Платні навчальні курси\" \nКурсова виконана на мові C# із застосуванням WindowsForms",
        disableNotification: true,
         replyMarkup: courseWorkInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "ClassesMenu")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Що саме ви хочете дізнатися?",
        disableNotification: true,
        replyMarkup: clasessInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "DemoMenu")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "На жаль, курсова у процесі розробки, тому секція код та скріншоти не працюють👨‍💻",
        disableNotification: true,
        replyMarkup: demoInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "MainMenu")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Головна сторінка",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Structure")
    {
        return await botClient.SendTextMessageAsync(
        chatId: update.CallbackQuery.From.Id,
        text: "Всю діаграму можно умовно розділити на “користувачі” та “курси”.\r\n\r\nГоловний класс у розділі “користувачі” - абстрактний класс User (користувач): він реалізує вбудований інтерфейс IComparble. \r\nВід абстрактного класу наслідуються чотири класси: UnAuthorised, Authorised, Teacher та Admin.\r\n - UnAuthorised - найпростіший, використовується перед тим, як користувач залогиниться/авторизується. Не має якогось явного функціоналу.\r\n - Authorised - звичайний користувач. Може покупати курси та проходити їх. Реалізує інтерфейс IStudyable.\r\n - Teacher - вчитель. Відрізняється від звичайного користувача тим, що не може проходити курси. Реалізує інтерфейс ITeacheble.\r\n - Admin - адміністратор. Також може створювати курси, але має більш обширний функціонал: може дивитися статистику курсів та створювати користувачів. Реалізує інтерфейс ITeacheble.\r\n\r\nУ розділі курси основний класс - Course (курс). Він має багато характеристик, одна з яких множина класу Module (модуль). У свою чергу один модуль містить у собі множину класу Lesson (урок, заняття).\r\n\r\nЦі два розділи об’єднує у собі класс CoursesApp (клас застосунку).\r\n\r\n\nПоміж класів є інтерфейси IStudyable та ITeacheble:\r\n - IStudyable містить функціонал навчання.\r\n - ITeacheble містить функціонал створення, редагування та моніторинг створених курсів\r\n",
        disableNotification: true,
        replyMarkup: clasessInlineKeyboard,
        cancellationToken: cancellationToken);
    }

    if (update.CallbackQuery.Data == "Diagram")
    {
        return await botClient.SendPhotoAsync(
        chatId: update.CallbackQuery.From.Id,
        photo: InputFile.FromUri("https://raw.githubusercontent.com/PemzaZevsa/TelegramBotLab2/master/Pics/Diogram.jpg"),
        caption: "<b>Діаграма класів</b>",
        replyMarkup: diagramInlineKeyboard,
        parseMode: ParseMode.Html,
        cancellationToken: cancellationToken);
    }


    return await botClient.SendTextMessageAsync(
    chatId: update.CallbackQuery.From.Id,
    text: "replyText",
    disableNotification: true,
    cancellationToken: cancellationToken);
}

async Task<Message> TextMessageReplyer(Update update, CancellationToken cancellationToken)
{
    string messageText = update.Message.Text;
    var chatId = update.Message.Chat.Id;

    if (messageText == "/start")
    {
        return await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Вітаю, цей бот був створений для опису функціоналу курсового проєкта. В описі бота є посилання на GitHub репозиторій проєкта.\nПосилання на GitHub репозиторій цього телеграм-бота також є у описі бота ",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }
    else if (messageText == "/help")
    {
        return await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "/start - Отримати початкове повідомлення\n/link - Отримати посилання на GitHub репозиторій бота",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }
    else if (messageText == "/link")
    {
        return await botClient.SendTextMessageAsync(
        chatId: chatId,
        parseMode: ParseMode.Html,
        text: "<a href=\"https://github.com/PemzaZevsa/TelegramBotLab2\">Посилання на GitHub репозиторій бота</a>",
        disableNotification: true,
        replyMarkup: baseInlineKeyboard,
        cancellationToken: cancellationToken);
    }
    else
    {
        return await botClient.SendTextMessageAsync(
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
}
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    string replyText = "";

    if (update.Type == UpdateType.CallbackQuery)
    {
        var replMessage = MesageReplyer(update, cancellationToken).Result;

        Console.WriteLine($"Name : {update.CallbackQuery.From.FirstName} Chat Id : {update.CallbackQuery.From.Id} at {update.CallbackQuery.Message.Date}");
        return;
    }
    else
    {
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

        var replMessage = TextMessageReplyer(update, cancellationToken).Result;

        ConsoleLogging(message);
    }
}

void ConsoleLogging(Message message)
{
    message.Date.ToLocalTime();

    Console.Write(
      $"{message.From?.FirstName} sent message {message.MessageId} : {message.Text} " +
      $"to chat {message.Chat.Id} at {message.Date}.\n");

    //Console.Write($"It is a reply to message {message.ReplyToMessage?.MessageId} : {message.ReplyToMessage?.Text} " +
    //  $"and has {message.Entities?.Length} message entities.\n");
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