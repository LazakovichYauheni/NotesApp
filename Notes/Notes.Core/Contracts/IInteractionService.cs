using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Core.Contracts
{
    public interface IInteractionService
    {
        EventHandler<NotesModel> ItemClicked { get; set; }
    }
}
