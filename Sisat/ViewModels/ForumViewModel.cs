using Sisat.Models;
namespace Sisat.ViewModels
{
    public class ForumViewModel : BaseViewModel 
    {
        public Forum Forum { get; set; }

        public List<Forum> Foruns { get; set; }

        public RespostasForum RespostaForum { get; set; }

        public List<RespostasForum> RespostasForuns { get; set; }

        public ForumViewModel()
        {
            Forum = new Forum();
            Foruns = new List<Forum>();

            RespostaForum = new RespostasForum();
            RespostasForuns = new List<RespostasForum>();
        }
    }
}

