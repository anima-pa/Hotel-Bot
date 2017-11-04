using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Hotel_Bot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi , I am Ann Bot");
            await Respond(context);
            context.Wait(MessageRecievedAsync);
        }
        private static async Task Respond(IDialogContext context)
        {
            var username = String.Empty;
            context.UserData.TryGetValue<string>("Name", out username);
            if (String.IsNullOrEmpty(username))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hi {0}, how can i help u?", username));
            }
        }
        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var username = String.Empty;
            var getname = false;

            context.UserData.TryGetValue<string>("Name", out username);
            context.UserData.TryGetValue<bool>("GetName", out getname);
            if (getname)
            {
                username = message.Text;
                context.UserData.SetValue<string>("Name", username);
                context.UserData.SetValue<bool>("GetName", false);

            }
            await Respond(context);
            context.Done(message);
        }
    }
}