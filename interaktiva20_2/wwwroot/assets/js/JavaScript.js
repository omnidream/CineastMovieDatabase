/*LIKES AND DISLIKES*/
let myImdbId;
let myCallerButton;
let myMovieObject;
let likeOrDislikeKey;
let allLikeButtons;
let cmdbUrl = 'https://cmdbapi.kaffekod.se/api/';
AddEventListenerToButtons(document.querySelectorAll('.btnLike'))
//AddEventListenerToButtons(allDislikeButtons) = AddEventListenerToButtons(document.querySelectorAll('.btnDislike'))

function AddEventListenerToButtons(buttons) {
    let i;
    for (i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("click", likeDislike)
    }
}


async function likeDislike() {
    myImdbId = this.dataset.imdbid;
    myCallerButton = this;
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
    let updateThisElement = findElementToUpdate(document.querySelectorAll('.likes'));
    updateThisElement.innerHTML = myMovieObject.numberOfLikes;

    //let updateThisElement = findElementToUpdate(document.querySelectorAll('.dislkies'));
    //updateThisElement.innerHTML = myMovieObject.numberOfDislkies;
}


function findElementToUpdate(myElements) {
    var i;
    for (i = 0; i < myElements.length; i++) {
        if (myElements[i].dataset.imdbid === myImdbId)
            return myElements[i];
    }
}

/*READ MORE PLOT*/

let readMoreText = document.querySelector('.moviePlot');
let link = document.querySelector('.more');
let numberOfShownChars = 120;

let shownText = readMoreText.innerHTML.slice(0, numberOfShownChars);
let hiddenText = readMoreText.innerHTML.slice(numberOfShownChars);

readMoreText.innerHTML = shownText + '<span class="moreDots"> ...</span> <span class="trimmedText">' + hiddenText + '</span>'


window.onclick = function (event) {
    if (event.target.className === 'more') {
        event.target.parentElement.parentElement.classList.toggle('showAll');
        console.log(readMoreText)
    }
}


