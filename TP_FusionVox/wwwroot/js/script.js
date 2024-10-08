
$(document).ready(function(){
    //formatage de l'adresse en petits ou grands caractères pour éviter les erreurs
    var pathToLower = window.location.pathname ? window.location.pathname.toLowerCase() : "";
    
    // ajoute un autre texte au texte du bouton en fonction de la page
    // function ajoutTextBtnUpsert(mot) {
    //     var textBtn = $('#btntextUpsert').text();
    //     $("#btntextUpsert").text(textBtn +" "+ mot);
    // }

    // vérification si l'adresse contient :create/edit et genremusical
    //changer en<i> à l'intérieur du <button> de type submit, class "fa-user-.."
    // l'icône dans la dropdown(header) restera intacte.

    if(pathToLower.includes("genremusical")){
        // ajoutTextBtnUpsert(" le G.M.")

        if(pathToLower.includes("create"))
            $("button[type='submit'] i.fa-user-plus").removeClass("fa-user-plus").addClass("fa-plus");
        else
            $("button[type='submit'] i.fa-user-pen").removeClass("fa-user-pen").addClass("fa-pen-to-square"); 
        
    }
    // else if (pathToLower.includes("artiste")) {
    //     ajoutTextBtnUpsert(" l'artiste")
    // }

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
    // //Ce switch modifie les classes du body ou de l'header en fonction du chemin de la page (ImageHero et marge)
    // var regex_a_id = /artiste\/\d+/;
    // var regex_id = /\d+/;
    // var regex_a_nom = /artiste\/[a-zA-Z]+(\/|$)/;
    // var artistNameRegex = /^(?!home$|artiste$|genremusical$|favoris$)[a-zA-Z]+$/;
    // switch (true) {
    //     case pathToLower.includes('recherche') || pathToLower.includes('filtre'):
    //       $('body').addClass('hero-image_Recherche');
    //       $('header').addClass('marge_header_bot_Recherche');
    //       break;
    //     case pathToLower.includes('detail')||regex_a_id.test(pathToLower) || regex_a_nom.test(pathToLower)|| regex_id.test(pathToLower)|| artistNameRegex.test(pathToLower):
    //       $('body').addClass('hero-image_Details');
    //       $('header').addClass('marge_header_bot_Details');
    //       break;
    //     case pathToLower.includes('favoris'):
    //         $('body').addClass('hero-image_Favoris');
    //         $('header').addClass('marge_header_bot_Favoris');
    //         break;
    //     case pathToLower.includes('home')||pathToLower.includes('home'):
    //         $('body').addClass('hero-image_Accueil');
    //         $('header').addClass('marge_header_bot_Accueil');
    //         break;
    //     case pathToLower.includes('create'):
    //         $('header').addClass('marge_header_bot_Ajouter');
    //         break;
    //     case pathToLower.includes('edit'):
    //         $('header').addClass('marge_header_bot_Modifier');
    //         break;
    //     case pathToLower.includes('delete'):
    //         $('header').addClass('marge_header_bot_Delete');
    //         break;
    //     default:
    //
    //   }
    
})

//Start DATATABLE
//================================================== 
$(document).ready(function () {
    $('#dataTable').DataTable({
        "language": {
            "decimal": ",",
            "thousands": ".",
            "lengthMenu": "Affichage de _MENU_ enregistrements par page",
            "zeroRecords": "Rien trouvé… Désolé! ",
            "info": "Affichage de la page _PAGE_ de _PAGES_",
            "infoEmpty": "Aucun enregistrement disponible ",
            "infoFiltered": "(filtré d’un maximum de _MAX_ enregistrement)",
            "search": "Recherche: ",
            "paginate": {
                "first": "Première",
                "last": "Dernière",
                "next": "Suivant",
                "previous": "Précédent",
            }

        }
    });
});
$(document).ready(function () {
    var paginateButtonsHorsSpan = $("a.paginate_button:not(span a.paginate_button)");
    paginateButtonsHorsSpan.removeClass('paginate_button').addClass('p1 mx-4')
    $('span a.paginate_button').removeClass('paginate_button').addClass('btn-rechercher')
});
//END DATATABLES
//================================================== 

//summernote
//================================================== 
$(document).ready(function () {
    $('.summernote').summernote({
        toolbar: [

            ['Menu', ['style', 'bold', 'italic', 'underline',
                'clear', 'fontname', 'fontsize', 'color', 'ul', 'ol',
                'paragraph', 'link', 'picture', 'video', 'fullscreen', 'codeview', 'help']],
        ],

        styleTags: ['p', 'pre', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'],
        fontNames: ['Montserrat', 'Arial', 'Courier New'],
        height: 130,
        minHeight: 65,
        maxHeight: 400,
        placeholder: "Saisir la biographie de l\'artiste",
        callbacks: {
            onInit: function () {
                var editor = elem.next(),
                    placeholder = editor.find(".note-placeholder");

                function isEditorEmpty() {
                    var code = elem.summernote("code");
                    return code === "<p><br></p>" || code === "";
                }

                editor.on("focusin focusout", ".note-editable", function (e) {
                    if (isEditorEmpty()) {
                        placeholder[e.type === "focusout" ? "show" : "hide"]();
                    }
                });
            }
        }
    })
});
//End Summernote
//================================================== 

/*Start PROGRESS: Buton Retour au début avec l'indicateur de progression
    ================================================== 
*/
(function ($) {
    "use strict";
    $(document).ready(function () {
        "use strict";
        //Scroll back to top
        var progressPath = document.querySelector('.progress-wrap path');
        var pathLength = progressPath.getTotalLength();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
        progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
        progressPath.style.strokeDashoffset = pathLength;
        progressPath.getBoundingClientRect();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
        var updateProgress = function () {
            var scroll = $(window).scrollTop();
            var height = $(document).height() - $(window).height();
            var progress = pathLength - (scroll * pathLength / height);
            progressPath.style.strokeDashoffset = progress;
        }
        updateProgress();
        $(window).scroll(updateProgress);
        var offset = 50;
        var duration = 550;
        jQuery(window).on('scroll', function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('.progress-wrap').addClass('active-progress');
            } else {
                jQuery('.progress-wrap').removeClass('active-progress');
            }
        });
        jQuery('.progress-wrap').on('click', function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        })


    });

})(jQuery);
/*END PROGRESS: Buton Retour au début avec l'indicateur de progression
    ================================================== 
*/

//Start - COUNTER
//   ==================================================
// animate each counter when element visible using intersection observer
// .counter inner text is the target number

if ("IntersectionObserver" in window) {
    let counterObserver = new IntersectionObserver(function (entries, observer) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                let counter = entry.target;
                let target = parseInt(counter.innerText);
                let step = target / 200;
                let current = 0;
                let timer = setInterval(function () {
                    current += step;
                    counter.innerText = Math.floor(current);
                    if (parseInt(counter.innerText) >= target) {
                        clearInterval(timer);
                    }
                }, 10);
                counterObserver.unobserve(counter);
            }
        });
    });

    let counters = document.querySelectorAll(".counter");
    counters.forEach(function (counter) {
        counterObserver.observe(counter);
    });
}
//END - COUNTER
//   ================================================== 
