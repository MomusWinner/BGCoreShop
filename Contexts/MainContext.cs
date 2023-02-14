using Core.Chapters;
using GameData;

namespace GameLogic.GameData.Contexts
{
    public class MainContext : BaseContext
    { 
        public Chapter CurrentChapter { get; private set; }

        public void SetChapter(Chapter chapter)
        {
            CurrentChapter = chapter;
        }
    }
}