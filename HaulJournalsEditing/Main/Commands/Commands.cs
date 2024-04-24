namespace HaulJournalsEditing.Main.Commands
{
    public class Commands
    {
        public SaveCommand SaveChanges { get; }

        public ReloadCommand Reload { get; }

        public LocalImportCommand ImportToLocal { get; }

        public ContinueVoyageCommand ContinueVoyage { get; }

        public ContinueStationCommand ContinueStation { get; }

        public ContinueHaulCommand ContinueHaul { get; }

        public BackToVoyagesCommand BackToVoyages { get; }

        public BackToStationsCommand BackToStations { get; }

        public BackToHaulsCommand BackToHauls { get; }

        public UploadLocalDataToRemoteCommand Upload { get; }

        public GoToDirectories GoToDirectories { get; }

        public SetObjectToNull SetObjectToNull { get; }

        public Commands(Main.MainViewModel mainViewModel)
        {
            SaveChanges = new SaveCommand(mainViewModel);
            Reload = new ReloadCommand(mainViewModel);
            ImportToLocal = new LocalImportCommand(mainViewModel);
            ContinueVoyage = new ContinueVoyageCommand(mainViewModel);
            ContinueStation = new ContinueStationCommand(mainViewModel);
            ContinueHaul = new ContinueHaulCommand(mainViewModel);
            BackToVoyages = new BackToVoyagesCommand(mainViewModel);
            BackToStations = new BackToStationsCommand(mainViewModel);
            BackToHauls = new BackToHaulsCommand(mainViewModel);
            Upload = new UploadLocalDataToRemoteCommand(mainViewModel);
            GoToDirectories = new GoToDirectories(mainViewModel);
            SetObjectToNull = new SetObjectToNull(mainViewModel);
        }

    }
}
