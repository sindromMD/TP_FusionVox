namespace TP_FusionVox.Utility
{
    public static class AppConstants
    {
        // Gestion des notifications Toastr 
        public const string Success = "Success";
        public const string Error = "Error";
        public const string Info = "Info";
        //Chemins d'accès pour les fichiers /images à partir du ROOT
        public static string ImagePath = @"\Images\";
        public static string ImagePathArtiste = ImagePath + @"Artistes\descktop\";
        public static string ImagePathArtisteCtrl = @"Images\Artistes\descktop";
        public static string ImagePathGenreMusical = ImagePath + @"Background\";
        public static string ImagePathGenreMuscialCtrl = @"Images\Background\";
        public static string ImageGenericArtiste = @"GenericArtiste.png";
        public static string ImageGenericGenreMusical = @"GenericGenreMusical.png";
    }

}
