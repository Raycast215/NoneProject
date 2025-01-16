using Template.Manager;

namespace NoneProject.Manager
{
    public class DataManager : SingletonBase<DataManager>
    {
        
        
#region Override Methods
        
        protected override void Initialized()
        {
            base.Initialized();
            
            isInitialized = true;
        }
        
#endregion
    }
}