
$(document).ready(function(){
    //formatage de l'adresse en petits ou grands caractères pour éviter les erreurs
    var pathToLower = window.location.pathname ? window.location.pathname.toLowerCase() : "";
    
    // ajoute un autre texte au texte du bouton en fonction de la page
    function ajoutTextBtnUpsert(mot) {
        var textBtn = $('#btntextUpsert').text();
        $("#btntextUpsert").text(textBtn +" "+ mot);
    }

    // vérification si l'adresse contient :create/edit et genremusical
    //changer en<i> à l'intérieur du <button> de type submit, class "fa-user-.."
    // l'icône dans la dropdown(header) restera intacte.

    if(pathToLower.includes("genremusical")){
        ajoutTextBtnUpsert(" le G.M.")

        if(pathToLower.includes("create"))
            $("button[type='submit'] i.fa-user-plus").removeClass("fa-user-plus").addClass("fa-plus");
        else
            $("button[type='submit'] i.fa-user-pen").removeClass("fa-user-pen").addClass("fa-pen-to-square"); 
        
    }
    else if (pathToLower.includes("artiste")) {
        ajoutTextBtnUpsert(" l'artiste")
    }

    // réutilisation de viewPartial[_FormAjoutAuFavorisButtonPartielle] pour 
    //l'ajouter aux favoris (modifier la taille et la position du bouton))
    if(pathToLower.includes("detail"))
    {
        $("div.btn-ajouter-coeur-card-position").css({
            "top": "15px",
            "right": "25px"
        });
        $("button.fa-heart-circle-plus").removeClass("fa-xl").addClass("fa-2xl");
    }
   
  
})