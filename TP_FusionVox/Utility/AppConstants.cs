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
        public static string ImagePathArtisteCtrl = @"Images\Artistes\descktop\";

        public static string ImagePathGenreMusical = ImagePath + @"GenresMusicaux\";
        public static string ImagePathGenreMusicalCtrl = @"Images\GenresMusicaux\";

        public static string ImagePathDetailsArtiste = ImagePath + @"Background\Details_Artistes\";
        public static string ImagePathDetailsArtisteCtrl = @"Images\Background\Details_Artistes\";
        //images génériques
        public static string ImageGenericArtiste = @"GenericArtiste.png";
        public static string ImageGenericGenreMusical = @"GenericGenreMusical.jpg";
        public static string ImageGenericDetailsArtiste = @"GenericDetailsArtiste.png";
    }

}
