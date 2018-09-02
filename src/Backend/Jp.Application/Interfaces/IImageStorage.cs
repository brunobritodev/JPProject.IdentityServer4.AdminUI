using System.Threading.Tasks;
using Jp.Application.ViewModels;

namespace Jp.Application.Interfaces
{
    public interface IImageStorage
    {
        Task<string> SaveAsync(ProfilePictureViewModel image);
    }

}
