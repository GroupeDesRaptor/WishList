using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liste
{

    public class Personne
    {
        public int Id;
        public string Nom;
        public List<ArticlePrefere> ListeCadeaux = new List<ArticlePrefere>();
        public decimal Cagnotte=1000;
		
        public void Identifier(string nom)
        {
            Nom = nom;
        }

        public void CreerListeCadeaux(ArticlePrefere a)
        {
            ListeCadeaux.Add(a);
        }

        internal void Cottiser (decimal Argent)
        {
            Cagnotte =Cagnotte + Argent;
        }

        internal List<ArticlePrefere> AfficherListeCadeaux(Personne p)
        {
            return p.ListeCadeaux;
        }
		
	    public List<Personne> ListeAmis;

        public static List<Personne> CreerListeAmis(List<Personne> listePara, Personne p)
        {
            listePara.Remove(p);
            List<Personne> listeReturn= listePara;

            return listeReturn;
        }
    }

    public class Article
    {
        public int Id;
        public string NomArticle;
        public decimal Prix=0;

        public Article(int id, string nomArticle, decimal prix)
        {
            Id = id;
	        NomArticle = nomArticle;
            Prix = prix;
        }
        public override string ToString()
        {
            //return "A votre wishlist a été ajouté l'article " + NomArticle + " qui coûte " + Prix + " euros."  ;
            return  NomArticle+ " , "+ Prix + " euros.";
        }
    }

    public class ArticlePrefere
    {
        public Personne personne;
        public Article ArticleAAjouter;
        public int Preference;
        
	    public ArticlePrefere(Article article, int preference)
        { 
			ArticleAAjouter=article;
			Preference= preference;
        }
		
        public override string ToString()
        {
            return "Vous donnez à l'article " + ArticleAAjouter.NomArticle + " la préférence " + Preference + " ." ;
        }
    }
}


