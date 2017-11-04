using Hotel_Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hotel_Bot.Dialogs
{
    [LuisModel("fab031b2-5cbb-4875-a473-eb331451fbfb", "1000c754ae6c443faae337f2d9242557")]
    [Serializable]
    public class LUISDialog:LuisDialog<RoomReservation>
    {
        private readonly BuildFormDelegate<RoomReservation> Reservations;
        public LUISDialog(BuildFormDelegate<RoomReservation> Reserve)
        {
            this.Reservations = Reserve;
        }
        [LuisIntent("")]
        public async Task None(IDialogContext context,LuisResult result)
        {
            await context.PostAsync("Sorry I didn't get what you mean..");
            context.Wait(MessageReceived);
        }
        
        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Reservations")]
        public async Task RoomReservation(IDialogContext context, LuisResult result)
        {
            var enrolment= new FormDialog<RoomReservation>(new RoomReservation(), this.Reservations, FormOptions.PromptInStart);
            context.Call<RoomReservation>(enrolment, Callback);
        }

        [LuisIntent("QueryAmenities")]
        public async Task QueryAmenities(IDialogContext context, LuisResult result)
        {
            foreach(var entity in result.Entities.Where(Entity=>Entity.Type=="Amenity"))
            {
                var value = entity.Entity.ToLower();
                if(value=="pool"|| value == "gym"|| value == "towel"|| value == "wifi")
                {
                    await context.PostAsync("Yes, We have that..");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("Sorry ,We don't have that");
                    context.Wait(MessageReceived);
                    return;
                }
            }
            await context.PostAsync("Sorry,We dont have that");
            context.Wait(MessageReceived);
            return;
        }
    }
}