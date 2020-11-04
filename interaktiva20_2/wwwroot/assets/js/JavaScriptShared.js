/*LIKES AND DISLIKES*/
let myImdbId;
let myCallerButton;
let myMovieObject;
let likeOrDislikeKey;
const cmdbUrl = 'https://cmdbapi.kaffekod.se/api/';
AddEventListenerToButtons(document.querySelectorAll('.btnLike'))
AddEventListenerToButtons(document.querySelectorAll('.btnDisike'))

function AddEventListenerToButtons(buttons) {
    let i;
    for (i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("click", likeDislike)
    }
}


async function likeDislike() {
    myCallerButton = this;
    myImdbId = myCallerButton.dataset.imdbid;
    likeOrDislikeKey = checkLikeDislike(myCallerButton)
    myCallerButton.disabled = true;
    myMovieObject = await GetCmdbApi();
    updateNumberOfLikesDislikes()
    myCallerButton.disabled = false;
}

function GetCmdbApi() {
    return fetch(cmdbUrl + myImdbId + likeOrDislikeKey)
        .then((response) => {
            if (response.ok)
                return response.json();
            else
                throw new Error('Something went wrong when calling CmdbAPI, sorry.');
        })
        .then(data => data)
        .catch((error) => { console.log(error) });
}

function checkLikeDislike(myCaller) {
    let result = '/dislike'
    if (myCaller.dataset.btnType === 'like')
        result = '/like'
    return result;
}

function updateNumberOfLikesDislikes() {
    let updateThisElement;

    updateThisElement = findElementToUpdate(document.querySelectorAll('.likes'));
    updateThisElement.innerHTML = myMovieObject.numberOfLikes;

    updateThisElement = findElementToUpdate(document.querySelectorAll('.dislikes'));
    updateThisElement.innerHTML = myMovieObject.numberOfDislikes;
}

//TODO: PRIO 4 Gör till foreach
function findElementToUpdate(myElements) {
    var i;
    for (i = 0; i < myElements.length; i++) {
        if (myElements[i].dataset.imdbid === myImdbId)
            return myElements[i];
    }
}

/* STICKY SEARCH ON SCROLL */
window.onscroll = function () { addStickyClass() };

let mySearchBar = document.querySelector('.searchContainer');
let sticky = mySearchBar.offsetTop;

function addStickyClass() {
    if (window.pageYOffset > sticky) 
        mySearchBar.classList.add("sticky");
    else 
        mySearchBar.classList.remove("sticky");
}


//TODO: PRIO 3 Generera knappar till likes och dislikes via js istället
//TODO: PRIO 2 Låt search följa med som fixed efter scroll
//TODO: PRIO 1 Få till searchfunktionen
//TODO: PRIO 1 Få till sökresultatssidan
//TODO: PRIO 1 Fixa responsiviteten
//TODO: PRIO 1 Se till så listorna visas snyggt på Home Index
//TODO: PRIO 1 Styla like-dislike-knapparna
//TODO: PRIO 1 Styla detaljsidan
//TODO: Prio 3 Städa bland CSS-filerna och byt namn på style_nav_search_bar till style_shared