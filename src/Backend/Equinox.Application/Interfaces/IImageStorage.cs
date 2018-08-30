using System.Threading.Tasks;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IImageStorage
    {
        Task<string> SaveAsync(ProfilePictureViewModel image);
    }

}
