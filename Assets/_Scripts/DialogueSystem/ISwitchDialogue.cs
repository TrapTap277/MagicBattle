using System.Threading.Tasks;

namespace _Scripts.DialogueSystem
{
    public interface ISwitchDialogue
    {
        Task SwitchDialogue(DialogueAnswerType whoWon, int count);
        void Fade();
    }
}