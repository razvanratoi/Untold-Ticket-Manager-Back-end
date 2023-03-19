using Assignment1.Models;

namespace Assignment1.Services.AbstractServices
{
    public interface AbstractCreator
    {
        void createFile(List<Ticket> tickets, Show show, Artist artist);
    }
}