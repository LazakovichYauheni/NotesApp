using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Core.Contracts
{
    public class InteractionService : IInteractionService
    {
        public EventHandler<NotesModel> ItemClicked { get; set; }
    }
}
