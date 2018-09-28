using System.ComponentModel.DataAnnotations;
using Jp.Domain.Commands.Client;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class SaveClientViewModel
    {
        /// <summary>
        /// Unique ID of the client
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// Client display name (used for logging and consent screen)
        /// </summary>
        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// URI to further information about client (used on consent screen)
        /// </summary>
        public string ClientUri { get; set; }

        /// <summary>
        /// URI to client logo (used on consent screen)
        /// </summary>
        public string LogoUri { get; set; }

        public string Description { get; set; }

        public ClientType ClientType { get; set; } = 0;

    }

}