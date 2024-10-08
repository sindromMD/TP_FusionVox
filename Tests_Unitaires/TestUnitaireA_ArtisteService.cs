﻿using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.Services;
using TP_FusionVox.ViewModels;

namespace Tests_Unitaires
{
    public class TestUnitaireA_ArtisteService
    {

        // Définir la DB InMemory

        private DbContextOptions<TP_FusionVoxDbContext> SetUpInMemory(string uniqueName)
        {
            var options = new DbContextOptionsBuilder<TP_FusionVoxDbContext>().UseInMemoryDatabase(uniqueName).Options;
            SeedInMemoryStore(options);
            return options;
        }
        // Créer une méthode void SeedInMemoryStore qui permet d'ajouter une Story bidon pour le test
        private async void SeedInMemoryStore(DbContextOptions<TP_FusionVoxDbContext> options)
        {
            using (var context = new TP_FusionVoxDbContext(options))
            {
                if (!context.Artistes.Any())
                {
                    context.Artistes.AddRange(
                     //POP
                     new Artiste() { Id = 1, Nom = "The Weeknd", IdGenreMusical = 1, AgentReprId = 1, ImageURL = "The_Weeknd.png", Pays = "Canada"/*, Agent = "Alexei Culaxiz"*/, DebutCarrier = new DateTime(2010, 01, 01), NbAbonnees = 433, NbChansons = 7, EstVedette = false, Biographie = "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020." },
                     new Artiste() { Id = 2, Nom = "Miley Cyrus", IdGenreMusical = 1, AgentReprId = 1, ImageURL = "Miley_Cyrus.png", Pays = "États-Unis"/*, Agent = "Trace Dempsey Cyrus"*/, DebutCarrier = new DateTime(2001, 03, 08), NbAbonnees = 745, NbChansons = 12, EstVedette = false, Biographie = "Destiny Cyrus1, dite Miley Cyrus, est une auteure-compositrice-interprète, musicienne, productrice et actrice américaine, née le 23 novembre 1992 à Nashville (Tennessee).\r\n\r\nElle rencontre la célébrité à l'adolescence en incarnant Miley Stewart / Hannah Montana dans une série de Disney Channel, Hannah Montana, où elle joue avec son père, le chanteur de country Billy Ray Cyrus, entre 2006 et 2011. Après avoir signé avec Hollywood Records, elle réalise son premier album, Meet Miley Cyrus (2007), qui est à la tête du Billboard 200 et qui est triple disque de platine après avoir été vendu à trois millions d'exemplaires. Grâce au succès de la franchise Hannah Montana, elle devient l'« idole des jeunes », selon le Daily Telegraph, et est surnommée « La reine de Disney », après les bonnes audiences de la série.\r\n\r\nEn 2007, elle entreprend également le Best of Both Worlds Tour, dans lequel elle interprète son propre rôle ainsi que celui d'Hannah Montana. La tournée connait un véritable succès, un film intitulé Hannah Montana et Miley Cyrus : Le Film concert évènement est tourné puis sorti directement en DVD en 2008 ; plus de 100 millions d'exemplaires en sont vendus à travers le monde. Elle sort son deuxième album, intitulé Breakout (2008), qui atterrit directement à la première place du Billboard 200 avec près de 400 000 exemplaires écoulés dès la première semaine, pour finir son exploitation à près de quatre millions à travers le monde en 2014. En 2009, Cyrus commence à adopter un style plus « adulte », présenté dans son EP The Time of our Lives, avec son single principal Party in the U.S.A., certifié septuple disque de platine sur le sol américain. Son image continue à évoluer à la suite du tournage de La Dernière Chanson, film dans lequel elle interprète un rôle plus sombre et plus sérieux." },
                     new Artiste() { Id = 3, Nom = "Bruno Mars", IdGenreMusical = 1, AgentReprId = 1, ImageURL = "Bruno_Mars.png", Pays = "États-Unis"/*, Agent = "Mark Ronson"*/, DebutCarrier = new DateTime(2004, 06, 11), NbAbonnees = 519, NbChansons = 10, EstVedette = false, Biographie = "Peter Gene Hernandez, dit Bruno Mars, né le 8 octobre 1985 à Honolulu, dans l'État de Hawaï aux États-Unis, est un auteur-compositeur-interprète, musicien, danseur-chorégraphe, directeur artistique, producteur, réalisateur, styliste et homme d’affaires américain.\r\n\r\nIl grandit dans une famille de musiciens et fait ses débuts dans la musique en produisant d'autres artistes, au travers de l'équipe de production The Smeezingtons avec Philip Lawrence et Ari Levine. Il est connu pour ses performances sur scène, sa mise en scène rétro et ses performances dans une grande variété de styles musicaux, notamment le RnB, le funk, la pop, la soul, le reggae, le hip hop et le rock. Bruno Mars est accompagné de son groupe, The Hooligans, qui joue de nombreux instruments, tels que la guitare électrique, la basse, le piano, les claviers, la batterie et les cuivres, et joue également des rôles de danseurs1.\r\n\r\nIl se fait connaître avec le titre Nothin' on You de B.o.B, puis Billionaire de Travie McCoy, avant la sortie mondiale de son premier album Doo-Wops and Hooligans en 2010. Il coécrit également de nombreux titres dont Right Round de Flo Rida, Wavin' Flag de K'naan ou Fuck You! de Cee Lo Green. En 2012, son deuxième album Unorthodox Jukebox (2012) est numéro un aux États-Unis, en Australie, au Canada, en Suisse et au Royaume-Uni, remportant un Grammy Awards du meilleur album vocal pop. Il contient les chansons Locked Out of Heaven, When I Was Your Man et Treasure, qui sont des succès internationaux. Le premier a remporté un Grammy Award pour la meilleure performance vocale pop masculine. En 2011, Mars a enregistré le single It Will Rain pour le film The Twilight Saga: Breaking Dawn - Part 1 (2011). En 2014, il intervient sur le morceau Uptown Funk de Mark Ronson. Deux ans plus tard, il sort son troisième album intitulé 24K Magic, axé sur le R & B. Le disque a fait ses débuts au numéro deux aux États-Unis, au Canada, en France et en Nouvelle-Zélande et a reçu sept Grammy Awards, remportant les principales catégories de l'album de l'année, du disque de l'année et de la chanson de l'année." },
                     new Artiste() { Id = 4, Nom = "Adele", IdGenreMusical = 1, AgentReprId = 1, ImageURL = "Adele.png", Pays = "Grande-Bretagne"/*, Agent = "Jack White"*/, DebutCarrier = new DateTime(2008, 09, 05), NbAbonnees = 399, NbChansons = 13, EstVedette = false, Biographie = "Adele Laurie Blue Adkins, dite Adele [əˈdɛl]1, née le 5 mai 1988 dans le quartier londonien de Tottenham, est une auteure-compositrice-interprète britannique.\r\n\r\nEn 2008, elle sort son premier album intitulé 19 qui se vend à plus de 7 millions d’exemplaires. Elle est la première à recevoir le prix Critics’ Choice (Prix de la Critique) des Brit Awards, distinguée « découverte de l’année 2008 » dans un vote des critiques musicales de la BBC, Sound of 2009. En 2009, Adele remporte deux prix de la 51e édition des Grammy Awards, celui du meilleur nouvel artiste et celui de la meilleure prestation pop féminine.\r\n\r\nEn 2011, elle sort son second album intitulé 21 qui se vend à plus de 31 millions d’exemplaires, dont 1,85 million en France. L'album a été le plus vendu dans le monde en 2011 et 2012. Il se classe 6e dans le classement des plus grands albums de tous les temps de la catégorie Women who rock par le magazine Rolling Stone et devient l'album le plus vendu au monde de cette décennie. Elle remporte six Grammy Awards lors de la 54e cérémonie, devenant la deuxième femme à accomplir un tel exploit. En 2012, elle interprète la chanson du film Skyfall, qui lui permet de remporter dans la catégorie Meilleure chanson originale un Golden Globe et un Oscar. En février 2013, elle remporte le Grammy Award de la meilleure performance pop solo pour Set Fire to the Rain." },
                     //HIP-HOP
                     new Artiste() { Id = 5, Nom = "Eminem", IdGenreMusical = 2, AgentReprId = 2, ImageURL = "Eminem.png", Pays = "États-Unis"/*, Agent = "Bassmint Production"*/, DebutCarrier = new DateTime(1996, 10, 12), NbAbonnees = 882, NbChansons = 10, EstVedette = true, Biographie = "Marshall Bruce Mathers III, dit Eminem, souvent stylisé EMINƎM, né le 17 octobre 1972 à Saint Joseph dans l'État du Missouri, est un rappeur américain, également producteur, acteur et fondateur de label. En plus de sa carrière solo, il fut aussi membre du groupe D12, dont il est le cofondateur, et compose le duo Bad Meets Evil avec Royce da 5'9\". Il a également fait partie, dans sa jeunesse, d'un groupe nommé Soul Intent, et a intégré temporairement le groupe Outsidaz dans la seconde moitié des années 1990.\r\n\r\nEminem a vendu plus de 227,5 millions de disques uniquement aux États-Unis, figurant ainsi parmi les artistes ayant vendu le plus de disques. Il est également l'artiste qui a vendu le plus d'albums aux États-Unis lors du xxie siècle. En incluant ses travaux avec D12 et Bad Meets Evil, il porte douze albums au sommet des classements américains. Aux États-Unis, Eminem possède trois albums solo certifiés diamant avec The Marshall Mathers LP, The Eminem Show et Curtain Call: The Hits ainsi que 3 singles certifiés diamant avec Lose Yourself, Not Afraid et Love the Way You Lie. Il compte plus de 600 récompenses, dont 15 Grammy Awards et un Oscar. Il est cité dans de nombreuses listes énumérant les meilleurs artistes de tous les temps, le magazine Rolling Stone l'ayant même déclaré « Roi du hip-hop ».\r\n\r\nEncore inconnu du grand public, Eminem publie son premier album, Infinite, en 1996. Le disque est un échec commercial, notamment à cause du fait qu'il a été produit en indépendant. C'est avec son premier extended play, nommé The Slim Shady EP (1997), qu'il commence à percer dans certaines sphères. Il n'obtient une popularité mondiale qu'après la sortie de son deuxième album, The Slim Shady LP (1999), le premier publié au label du producteur et rappeur Dr. Dre, Aftermath Records. " },
                     new Artiste() { Id = 6, Nom = "Carla's Dream", IdGenreMusical = 2, AgentReprId = 2, ImageURL = "Carlas_Dream.png", Pays = "Moldavie"/*, Agent = "Cristi Ochiu"*/, DebutCarrier = new DateTime(2012, 05, 07), NbAbonnees = 219, NbChansons = 10, EstVedette = true, Biographie = "Carla's Dreams est un projet musical moldave qui a débuté en 2012. C'est un groupe de chanteurs et compositeurs anonymes, qui chantent en roumain, en russe et anglais. Lors des concerts, le chanteur du groupe, afin de cacher son identité, porte une capuche et des lunettes de soleil, ainsi son visage est masqué.\r\n\r\nCréé à Chișinău, Carla's Dreams combine plusieurs styles musicaux, y compris le hip-hop, le jazz, le rock et la pop. La première chanson produite par Carla's Dreams était Dă-te (Dégage). Carla's Dreams a été lancé en Roumanie en 2013, avec Inna la chanson P.O.H.U.I. Plus tard le groupe a chanté avec Loredana Lumea ta (Votre Monde), et en 2015 avec Delia pour la chanson Cum ne noi (Comment Nous Avons).\r\n\r\nEn 2016, alors que leur prochain album NGOC est en préparation, le groupe produit plusieurs chansons qui connaissent un grand succès, telles que Sub Pielea Mea (Sous ma peau), Acele (« Aiguilles » ou « celles »), Unde (Où), Imperfect (Imparfait) ou encore une chanson en russe Треугольники (Triangles). Carla's Dreams s'exporte du même coup et commence à être populaire en dehors des frontières moldaves, en Roumanie, Ukraine et Russie notamment (où le single Sub Pielea Mea reste en tête du classement du Top Hit 100 de juin à septembre 2016). Le 5 septembre 2016, Carla's Dreams a inauguré la saison des concerts dans le stade du Zimbru, à Chisinau, en se produisant devant 10 000 personnes. Le 6 septembre 2016, Carla's Dreams publie la chanson Imperfect accompagnée du clip sur leur chaîne Youtube, déclenchant un succès immédiat, et un record pour eux, puisque la vidéo a été visionnée un million de fois en 24 heures, et deux millions en 48 heures." },
                     new Artiste() { Id = 7, Nom = "Lil Nas X", IdGenreMusical = 2, AgentReprId = 2, ImageURL = "Lil_Nas_X.png", Pays = "États-Unis"/*, Agent = "Lithia Springs"*/, DebutCarrier = new DateTime(2019, 07, 21), NbAbonnees = 349, NbChansons = 6, EstVedette = false, Biographie = "Lil Nas X, de son vrai nom Montero Lamar Hill, né le 9 avril 1999 à Lithia Springs, en Géorgie, est un rappeur, auteur-compositeur-interprète et danseur-chorégraphe américain.\r\n\r\nIl se fait mondialement connaître en 2019 avec la chanson Old Town Road, mélange de country et de rap, qui devient le titre ayant passé le plus de semaines à la première place du Billboard Hot 100 américain, puis avec plusieurs clips dans lesquels il affirme son homosexualité de façon militante et décomplexée. Il sort son premier album, Montero, en 2021.\r\n\r\n" },
                     new Artiste() { Id = 8, Nom = "NF", IdGenreMusical = 2, AgentReprId = 2, ImageURL = "NF.PNG", Pays = "États-Unis"/*, Agent = "Nathan Feuerstein"*/, DebutCarrier = new DateTime(2010, 11, 09), NbAbonnees = 119, NbChansons = 7, EstVedette = false, Biographie = "Nathan John Feuerstein (prononcer « Fire-stein ») est un rappeur américain, artiste et auteur-compositeur né le 30 mars 1991 et connu sous le nom de scène NF. Connu pour ses chansons très personnelles et aux émotions fortes, c'est à la sortie de son troisième album, Perception, qu'il connaît un franc succès aux États-Unis et dans le monde entier, notamment grâce à son single Let You Down.\r\n\r\nIl sort un EP en 2014 chez Capitol CMG, NF. Cet événement marque son entrée au hit-parade du magazine Billboard. S'ensuivent quatre albums studio : Mansion en 2015, Therapy Session en 2016, Perception en 2017, The Search en 2019. Ainsi que la mixtape CLOUDS en 2021. Il se marie en 2018 avec sa meilleure amie de longue date, Bridgette Doremus, union de laquelle naîtra Beckham John Feuerstein le 13 août 2021." },
                     //Électro
                     new Artiste() { Id = 9, Nom = "Tiesto", IdGenreMusical = 3, AgentReprId = 3, ImageURL = "tiesto.png", Pays = "Pays-Bas"/*, Agent = "Black Hole Recordings"*/, DebutCarrier = new DateTime(2000, 05, 11), NbAbonnees = 419, NbChansons = 22, EstVedette = true, Biographie = "Tiësto, nom de scène de Tijs Michiel Verwest, est un DJ, compositeur et producteur néerlandais né le 17 janvier 1969 à Bréda. Il est considéré comme une grande figure de la musique électronique, aux côtés notamment de Paul van Dyk, Calvin Harris, Hardwell, David Guetta et Armin van Buuren ; DJ Magazine le classe « plus grand DJ de tous les temps » et Mixmag meilleur DJ du monde.\r\n\r\nVerwest commence à travailler en 1994 comme vendeur dans un magasin de disques à Rotterdam, puis devient disc jockey lors de son temps libre. Avec sa réorchestration du titre Silence de l'artiste Delerium en 2000, sa carrière est lancée. Étant dans les années 1990 assimilé au courant trance, il change progressivement de style et, depuis 2010, ses titres sont plus tournés vers l'EDM. Il participe à la cérémonie d'ouverture des Jeux olympiques d'Athènes en 2004 et anime la ville de Pékin durant ceux de 2008. Pendant l'Euro 2012, il donne plusieurs concerts en Pologne et en Ukraine, à la demande du comité d'organisation. Il présente depuis 2007 son podcast hebdomadaire, Club Life, un set musical d'une durée de deux heures. En 2015, il est récompensé pour sa réorchestration du titre All of Me de l'artiste américain John Legend lors de la 57e cérémonie des Grammy Awards.\r\n\r\nVerwest est également connu pour sa philanthropie, reversant une partie de ses revenus à des ONG promouvant divers programmes éducatifs ou médicaux. Selon un classement paru en 2017, il est l'artiste ayant parcouru le plus de kilomètres au cours de ses tournées et apparitions lors de festivals avec 2 505 953 kilomètres." },
                     new Artiste() { Id = 10, Nom = "Marshmello", IdGenreMusical = 3, AgentReprId = 3, ImageURL = "Marshmello.png", Pays = "États-Unis"/*, Agent = "Monstercat"*/, DebutCarrier = new DateTime(2015, 03, 03), NbAbonnees = 659, NbChansons = 15, EstVedette = false, Biographie = "Marshmello, de son vrai nom Christopher Comstock, né le 19 mai 1992 à Philadelphie en Pennsylvanie, est un DJ, producteur et compositeur américain.\r\n\r\nIl rencontre le succès en 2016 avec le titre Alone et son clip qui totalise plus de 2,2 milliards de vues sur YouTube, puis par ses morceaux Happier, Wolves, Friends, et Silence certifiés disques de platine et ses collaborations avec Lil Peep, Selena Gomez, Halsey, Juice WRLD ou encore Khalid.\r\n\r\nEn 2019, il est classé 2e DJ le mieux payé au monde par le magazine Forbes et 5e meilleur DJ au monde d'après le classement par vote établi par DJ Mag." },
                     new Artiste() { Id = 11, Nom = "Daft Punk", IdGenreMusical = 3, AgentReprId = 3, ImageURL = "Daft_Punk.png", Pays = "France"/*, Agent = "Cristi Ochiu"*/, DebutCarrier = new DateTime(1993, 06, 03), NbAbonnees = 919, NbChansons = 17, EstVedette = true, Biographie = "Daft Punk est un groupe de musique électronique français, originaire de Paris. Composé de Thomas Bangalter et Guy-Manuel de Homem-Christo, le groupe est actif entre 1993 et 2021 et participe à la création et à la démocratisation du mouvement de musique électronique baptisé French touch. Le duo fait partie des artistes français s'exportant le mieux à l'étranger et ce, depuis la sortie de leur premier véritable succès, Da Funk, en 1995.\r\n\r\nUne des originalités de Daft Punk est la culture de leur notoriété d'artistes indépendants et sans visage, portant toujours en public des casques et des costumes. Ils s'inspirent sur ce point du film Phantom of the Paradise de Brian De Palma. Daft Punk sort son premier album intitulé Homework en 1997. Le second album, commercialisé en 2001, s'intitule Discovery. Il comprend des succès tels que One More Time, Digital Love et Harder, Better, Faster, Stronger.\r\n\r\nUn troisième album voit le jour en 2005, et est nommé Human After All. Le duo a également composé en 2010 la bande son du film Tron : L'Héritage. En 2013, Daft Punk quitte EMI Records pour signer avec le label Columbia Records et sortir un album intitulé Random Access Memories, qui remporte un important succès international et cinq Grammy Awards, dont celui du meilleur album de l'année, alors que la chanson Get Lucky triomphe dans le monde entier. Le 22 février 2021, ils annoncent la séparation de leur duo dans une vidéo intitulée Epilogue, après vingt-huit ans de carrière." },
                     new Artiste() { Id = 12, Nom = "David Guetta", IdGenreMusical = 3, AgentReprId = 3, ImageURL = "David_Guetta.png", Pays = "France"/*, Agent = "Warner Music Group"*/, DebutCarrier = new DateTime(1986, 07, 05), NbAbonnees = 419, NbChansons = 20, EstVedette = true, Biographie = "David Guetta, nom de scène de Pierre David Guetta, est un DJ, compositeur, producteur français né le 7 novembre 1967 à Paris. Il débute adolescent avant de se professionnaliser juste avant la majorité. Il se fait connaître au début de sa carrière comme une figure des nuits parisiennes en faisant ses premières armes dans divers lieux parisiens vers la fin des années 1980. Par la suite, il crée ses propres soirées à Ibiza.\r\n\r\nDès 2007, il acquiert une reconnaissance internationale avec ses albums Pop Life et One Love. Dès lors, plusieurs de ses titres comme When Love Takes Over, Sexy Bitch, ou Gettin' Over You se classent en tête des ventes à travers le monde. Depuis, sa renommée ne cesse de croitre, démontrée par ses records de ventes, sa capacité à remplir les plus grands lieux lors de ses prestations ou par son nombre d'abonnés sur les réseaux sociaux. Entré dans le classement en 2005, il se voit d'ailleurs élu six ans plus tard « DJ le plus populaire du monde » par le magazine DJ Mag.\r\n\r\nDe grands noms de la scène urbaine collaborent sur la majorité de ses derniers singles mais il participe aussi à l'introduction de nouveaux talents. En 2013, le titre When Love Takes Over de David Guetta en collaboration avec Kelly Rowland est élu meilleure collaboration pop-dance de tous les temps par le magazine Billboard. À l'été 2018, après la sortie de son septième album, 7, David Guetta totalise plus de 13 millions d'albums et 81 millions de singles vendus. Mondialement connu, il est le compositeur de l'hymne de l'Euro 2016 se déroulant en France.\r\n\r\nIl est également le Français le plus suivi sur Youtube avec plus de 25,5 millions d'abonnés." }
                        );

                    context.genresMusicaux.AddRange(
                        new GenreMusical() { Id = 1, Nom = "POP", ImageUrl = "POP.jpg", Description = "La musique pop, est un style qui a conquis les cœurs du monde entier grâce à son caractère accrocheur et entraînant. Nous vous invitons à découvrir nos artistes pop et à vous laisser emporter par leurs mélodies captivantes et leurs performances éblouissantes." },
                        new GenreMusical() { Id = 2, Nom = "HIP-HOP", ImageUrl = "Hip-Hop.jpg", Description = "En concentrant nos efforts sur le hip-hop, nous cherchons à promouvoir une musique passionnante et significative, à soutenir des artistes talentueux et à contribuer à l'évolution de la culture musicale. Nous croyons en la puissance du hip-hop pour inspirer, éduquer et unir les gens, et nous sommes fiers de faire partie de cette communauté créative en constante expansion." },
                        new GenreMusical() { Id = 3, Nom = "ÉLECTRO", ImageUrl = "electro.jpg", Description = "La musique électronique est une force mondiale, transcendant les frontières et les cultures. Elle rassemble des fans et des artistes du monde entier, créant une communauté diversifiée et passionnée. En nous concentrant sur la musique électronique, nous souhaitons promouvoir l'échange culturel et offrir une plateforme internationale aux artistes électroniques talentueux." }
                        );
                    context.Concert.AddRange(
                        new Concert() { Id = 1, Nom = "Hope Tour", DateConcert = new DateTime(2023, 10, 3, 20, 00, 00), Pays = "Canada", Ville = "Laval", Scene = "Place Bell", NbTotalBillets = 1000, NbBilletsVendu = 123, PrixBillet = 100.25M, ImageUrl = "", Description = "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement." },
                        new Concert() { Id = 2, Nom = "Guitar Story", DateConcert = new DateTime(2023, 10, 21, 20, 00, 00), Pays = "Canada", Ville = "Brossard", Scene = "Le Club Square Dix30", NbTotalBillets = 323, NbBilletsVendu = 56, PrixBillet = 35.99M, ImageUrl = "", Description = "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement." },
                        new Concert() { Id = 3, Nom = "MetalCore", DateConcert = new DateTime(2023, 10, 31, 20, 00, 00), Pays = "Canada", Ville = "Montreal", Scene = "L'Etoile", NbTotalBillets = 600, PrixBillet = 45.23M, NbBilletsVendu = 72, ImageUrl = "", Description = "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement." }
                    );
                    context.Agent.AddRange(
                        new Agent() { Id = 1, DonneesConfidentiellesAgentID = 1, Nom = "Alex Culaxiz", ImageURL = "", Pays = "Canada", DateNaissance = new DateTime(1999, 01, 01), SalaireMensuel = 3564.50F, Curriel = "Alexei@vox.ca", Biographie = "Le professionnalisme de Alexei Culaxiz se reflète dans sa capacité à naviguer dans les complexités des contrats et des négociations, ce qui permet d'obtenir des avantages optimaux pour les artistes et la maison de disques. Sa riche expérience et sa connaissance approfondie de l'industrie ont fait de lui une source d'inspiration pour les artistes comme pour ses collègues." },
                        new Agent() { Id = 2, DonneesConfidentiellesAgentID = 2, Nom = "Simion Beblea", ImageURL = "", Pays = "Romania", DateNaissance = new DateTime(1990, 01, 01), SalaireMensuel = 4268.10F, Curriel = "Simion@vox.ca", Biographie = "Connu pour son grand professionnalisme, sa passion indéniable et sa capacité à transformer les rêves des artistes en réalité, Simion Beblea est un agent exceptionnel dans l'industrie musicale. Avec une carrière solide et des réalisations impressionnantes, Simion a consolidé sa réputation de mentor dévoué et de soutien aux artistes." },
                        new Agent() { Id = 3, DonneesConfidentiellesAgentID = 3, Nom = "Mirza Iurii", ImageURL = "", Pays = "Moldova", DateNaissance = new DateTime(1998, 01, 01), SalaireMensuel = 9856.99F, Curriel = "Iurii@vox.ca", Biographie = "Avec un dévouement inébranlable et une vision large de l'industrie musicale, Mirza Iurii continue à ajouter de la valeur à chaque projet dans lequel il s'implique. Qu'il s'agisse de jeunes talents ou d'artistes confirmés, Iurie est reconnu pour sa capacité à guider les carrières vers de nouveaux sommets de réussite." }
                        );
                    context.DonneesConfidentiellesAgent.AddRange(
                         new DonneesConfidentiellesAgent() { Id = 1, BankAccountInfo = "1278162365478963", NumeroDeContrat = "2522-45445576" },
                         new DonneesConfidentiellesAgent() { Id = 2, BankAccountInfo = "8904589765450963", NumeroDeContrat = "2252-44745655" },
                         new DonneesConfidentiellesAgent() { Id = 3, BankAccountInfo = "4255897697478343", NumeroDeContrat = "2426-48244589" }
                      );
 
                    context.SaveChanges();
                }

            }

        }

        [Fact]
        public async Task ObtenirToutListAsyncReturnList()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("ObtenirToutListAsyncReturnList")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);

                var result = await artisteService.ObtenirToutListAsync();
                var artisteInResult = result.Count();
                var artisteInBD = dbTest.Artistes.Count();
                Assert.NotNull(result);
                Assert.Equal(artisteInBD, artisteInResult);
                Assert.IsType<List<Artiste>>(result);
            }
        }
        [Fact]
        public async Task ObtenirToutParIdAsync_ReturnList_Selon_GenreMusical()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("ObtenirToutParIdAsync_ReturnList_Selon_GenreMusical")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);
                var genreMusical = dbTest.genresMusicaux.First();
                var nbArtisteGMFirst = dbTest.Artistes.Where(x => x.IdGenreMusical == genreMusical.Id).Count();
                var result = await artisteService.ObtenirToutParIdAsync(genreMusical.Id);
                var artisteInResult = result.Count();
                Assert.NotNull(result);
                Assert.Equal(nbArtisteGMFirst, artisteInResult);
                Assert.IsType<List<Artiste>>(result);
            }
        }
        [Fact]
        public async Task ObtenirArtisteParNomAsync_ReturnArtiste()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("ObtenirArtisteParNomAsync_ReturnArtiste")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);
                var artisteAChercher = await dbTest.Artistes.LastOrDefaultAsync();
                var nomArtisteInexistant = ""; 
                var result1 = await artisteService.ObtenirArtisteParNomAsync(artisteAChercher.Nom);
                var result2 = await artisteService.ObtenirArtisteParNomAsync(nomArtisteInexistant);

                Assert.NotNull(result1);
                Assert.IsType<Artiste>(result1);
                Assert.Equal(artisteAChercher, result1);

                Assert.Null(result2);
            }
        }

        [Fact]
        public async Task FiltrageArtiste_ReturnListArtiste()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("FiltrageArtiste_ReturnListArtiste")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);
                var criteres = new CritereRechercheViewModel
                {
                    EstGenreMusicalPOP = true,
                    EstGenreMusicalHipHop = false,
                    EstGenreMuscialElectro = false,
                    ChoiPourPersonnageVedette = "Non",
                    NbMinAbonnes = 100,
                    NbMaxAbonnes = 1000,
                    InputNomArtiste = "Adele"
                };

                var result = artisteService.FiltrageArtiste(criteres);
                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<Artiste>>(result);
                Assert.Equal(1, result.Count());

            }
        }
        [Fact]
        public async Task ArtisteNomExist_RetunrTrueOrFalse()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("ArtisteNomExist_RetunrTrueOrFalse")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);
                var nomArtiste_Vrai = "Lil Nas X";
                var nomArtiste_Faux = "Alexei";
                var resultV = artisteService.ArtisteNomExist(nomArtiste_Vrai);
                var resultF = artisteService.ArtisteNomExist(nomArtiste_Faux);
                Assert.NotNull(resultV);
                Assert.NotNull(resultF);
                Assert.IsType<bool>(resultV);
                Assert.IsType<bool>(resultF);
                Assert.Equal(true, resultV);
                Assert.Equal(false, resultF);
            }
        }
        [Fact]
        public async Task ClearConcertsAsync_ReturnListVide()
        {
            using (var dbTest = new TP_FusionVoxDbContext(SetUpInMemory("ClearConcertsAsync_ReturnListVide")))
            {
                IArtisteService artisteService = new ArtisteService(dbTest);
                var artiste = await artisteService.ObtenirArtisteParNomAsync("NF");
                artiste.ListConcerts = new List<Concert>
                {
                    dbTest.Concert.FirstOrDefault(),
                    dbTest.Concert.LastOrDefault()
                };
                artisteService.ClearConcertsAsync(artiste.Id);

                Assert.Empty(artiste.ListConcerts);

            }
        }
    } 
}
