﻿using Liste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTM.Cadeau.UIL
{


    class Program
    {
        static string DemanderString(string pMessage)
        {
            string valeur = "";

            while (valeur.Trim().Length == 0)
            {
                Console.WriteLine(pMessage);
                valeur = Console.ReadLine();
            }

            return valeur;
        }

        static int AfficherMenuChoixEntier(string pMessage, List<string> pListeChoix)
        {
            int choix = 0;
            Console.WriteLine(pMessage);

            for (int numChoix = 0; numChoix < pListeChoix.Count; numChoix++)
            {
                Console.WriteLine("[" + "Id=" + (numChoix + 1) + "] " + pListeChoix[numChoix]);
            }

            Console.Write("Votre choix (1-" + pListeChoix.Count + ") ? ");

            while (!int.TryParse(Console.ReadLine(), out choix) || choix < 1 || choix > pListeChoix.Count)
            {
                Console.WriteLine("Erreur! Entrez un nombre entre 1 et " + pListeChoix.Count + " :");
            }

            return choix - 1;
        }

        static int AfficherMenuIdentification(List<Personne> pListePersonnes)
        {
            List<string> listeChoix = new List<string>();
            foreach (Personne pers in pListePersonnes)
            {
                listeChoix.Add(pers.Nom);
            }
            listeChoix.Add("Saisir un autre nom...");

            int choix = AfficherMenuChoixEntier("Bienvenue!\nVeuillez vous identifier :", listeChoix);

            if (choix == listeChoix.Count - 1)
            {
                pListePersonnes.Add(
                    new Personne { Nom = DemanderString("Votre nom ?") }
                );
            }

            return choix;
        }


        public enum ChoixMenu { ConstitutionListeCadeaux = 1, Cotisation = 2, ChoixCadeaux = 3, Quitter = 0 }

        static void Main(string[] args)
        {
            List<Article> listeArticle = new List<Article>
            {
               new Article(1, "Ordinateur", 1000),
               new Article(2, "téléphone portable", 999),
               new Article(3,"appareil photo", 800),
               new Article(4, "console jeux vidéo", 400),
               new Article(5, "voyage en Chine", 1700),
               new Article(6, "livre C# pour les nulls", 30),
               new Article(7, "chaussures", 149),
               new Article(8, "carte cadeau Darty", 100),
               new Article(9, "montre", 700),
               new Article(10, "disque Blue Ray", 29)
            };

            List<Personne> listePersonnes = new List<Personne>
            {
                new Personne { Id=1, Nom = "Guillaume" },
                new Personne { Id=2, Nom = "Joao" },
                new Personne { Id=3, Nom = "Qing" }
            };

            // Etape 1 : constitution de la liste SANS préférence

            // Choix de l'utilisateur courant parmi les personnes dans la liste, ou ajout d'un nouvel utilisateur
            int idUtilisateur = AfficherMenuIdentification(listePersonnes);


            Console.WriteLine("Bonjour " + listePersonnes[idUtilisateur].Nom + "!");
            Console.WriteLine("Veuillez choisir votre menu");

            int choixMenu = 1;
            int j = 0;
            while (choixMenu != 0 || j == 0)
            {
                Console.WriteLine(" [1] Je constitue ma Wishlist\n [2] Je cotise pour mes amis\n [3] Je choisis mes cadeaux \n [0] Quitter");
                string saisie = Console.ReadLine();
                if (!int.TryParse(saisie, out choixMenu) || choixMenu < 0 || choixMenu > 3)
                {
                    Console.WriteLine("Recommencez !");
                    j = 0;
                }
                j = 1;

                ChoixMenu choixMenuEnum = (ChoixMenu)choixMenu;

                switch (choixMenuEnum)
                {
                    case ChoixMenu.ConstitutionListeCadeaux:
                        {
                            Console.WriteLine("Veuillez créer votre liste de cadeaux");

                            for (int numArticle = 0; numArticle < listeArticle.Count; numArticle++)
                           { Console.WriteLine("[" + (numArticle + 1) + "] " + listeArticle[numArticle]); }

                            Console.WriteLine("Combien d'articles voulez vous ajouter dans la liste ?");
                            string nbArticleStr = Console.ReadLine();

                            int nbArticle;
                            while (!int.TryParse(nbArticleStr, out nbArticle))
                            {
                                Console.WriteLine("erreur! Recommencez !");
                                nbArticleStr = Console.ReadLine();
                            }

                            Console.WriteLine("Le nombre rentré est correct");

                            List<ArticlePrefere> listePreferee = new List<ArticlePrefere>();

                            for (int i = 0; i < nbArticle; i++)
                            {
                                Console.WriteLine("Ajouter l' ID de l'article");

                                int idInt;
                                string idStr;
                                do
                                {
                                    idStr = Console.ReadLine();
                                    if (!int.TryParse(idStr, out idInt))
                                        Console.WriteLine("erreur! Recommencez !");

                                } while (!int.TryParse(idStr, out idInt));

                                Article articleAAjouter = listeArticle[idInt - 1];
                                Console.WriteLine(articleAAjouter.ToString());


                                // On garde les données rentrées dans l'ordinateur dans une variable article 


                                // On veut associer un article à une préférence 
                                Console.WriteLine("Veuillez ajouter un nombre entre 1 et 5 pour donner votre préférence");

                                int pref;
                                string prefStr;
                                do
                                {
                                    prefStr = Console.ReadLine();
                                    if (!int.TryParse(prefStr, out pref))
                                        Console.WriteLine("erreur! Recommencez !");

                                } while (!int.TryParse(prefStr, out pref));

                                ArticlePrefere articlePrefere = new ArticlePrefere(articleAAjouter, pref);
                                Console.WriteLine(articlePrefere.ToString());
                                // On ajoute la variable à la liste de cadeau
                                listePersonnes[idUtilisateur].CreerListeCadeaux(articlePrefere);

                                if (i < (nbArticle - 1))
                                    Console.WriteLine("Veuillez entrer le cadeau suivant");
                            }

                        }
                        break;

                    case ChoixMenu.Cotisation:
                        {
                            var requete1 = from p in listePersonnes
                                           where p.Id != (idUtilisateur + 1)
                                           select p;

                            foreach (var item in requete1)
                            {
                                Console.WriteLine("[Id=" + item.Id + "]" + "  Je cotise pour " + item.Nom);
                            }

                            Console.WriteLine("Veuillez choisir l'ID de votre ami");
                            int idAmis;
                            string idAmisStr;
                            do
                            {
                                idAmisStr = Console.ReadLine();
                                if (!int.TryParse(idAmisStr, out idAmis))
                                    Console.WriteLine("erreur! Recommencez !");

                            } while (!int.TryParse(idAmisStr, out idAmis));

                            var requete2 = from p in listePersonnes
                                           where p.Id == idAmis
                                           select p;

                            foreach (var item in requete2)
                            {
                                Console.WriteLine("Combien voulez-vous donner à votre ami {0} ?", item.Nom);

                                decimal argent;
                                string argentStr;
                                do
                                {
                                    argentStr = Console.ReadLine();
                                    if (!decimal.TryParse(argentStr, out argent))
                                        Console.WriteLine("erreur! Recommencez !");

                                } while (!decimal.TryParse(argentStr, out argent));

                                item.Cagnotte = item.Cagnotte + argent;

                                Console.WriteLine("{0} possède {1} euro.", item.Nom, item.Cagnotte);
                            }
                            break;
                        }

                    case ChoixMenu.ChoixCadeaux:
                        {

                            var articlesPrefOrdonnes = from item in listePersonnes[idUtilisateur].ListeCadeaux
                                                       orderby item.Preference descending
                                                       select item;

                            Console.WriteLine("Voici votre liste de cadeaux, triée par liste de préférence");

                            foreach (var item in articlesPrefOrdonnes)
                            {
                                Console.WriteLine("{0}, {1}", item.ArticleAAjouter.NomArticle, item.Preference);
                            }

                            List<ArticlePrefere> ListeDefinitive = new List<ArticlePrefere>();

                            bool i = true;
                            while (i)
                            {
                                foreach (var item in articlesPrefOrdonnes)
                                {
                                    if (listePersonnes[idUtilisateur].Cagnotte > 0 && listePersonnes[idUtilisateur].Cagnotte > item.ArticleAAjouter.Prix)
                                    {
                                        ListeDefinitive.Add(item);

                                        listePersonnes[idUtilisateur].Cagnotte = listePersonnes[idUtilisateur].Cagnotte - item.ArticleAAjouter.Prix;

                                    }
                                }
                                i = false;
                            }

                            Console.WriteLine("Voici ainsi votre liste de cadeaux :) ");
                            Console.WriteLine("Il vous reste {0} euro", listePersonnes[idUtilisateur].Cagnotte);
                        }

                        break;

                    default: Console.WriteLine("bye"); break;
                }
            }
            Console.Read();
        }//main

        private static List<Personne> CreerListeAmis(List<Personne> listePara, int p)
        {
            listePara.RemoveAt(p);
            List<Personne> listeReturn = listePara;

            return listeReturn;
        }
    }//program
}//namespace
