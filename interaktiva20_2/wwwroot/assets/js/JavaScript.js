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

//TODO: Gör till foreach PRIO 3
function findElementToUpdate(myElements) {
    var i;
    for (i = 0; i < myElements.length; i++) {
        if (myElements[i].dataset.imdbid === myImdbId)
            return myElements[i];
    }
}


/*READ MORE PLOT*/

let readMoreText = document.querySelectorAll('.moviePlot');
let link = document.querySelector('.more');
console.log(link);
let numberOfShownChars = 120;

readMoreText.forEach(plot => {
    let shownText = plot.innerHTML.slice(0, numberOfShownChars);
    let hiddenText = plot.innerHTML.slice(numberOfShownChars);
    plot.innerHTML = shownText + '<span class="moreDots"> ...</span> <span class="trimmedText">' + hiddenText + '</span>';
})

document.onclick = function (event) {
    if (event.target.className === 'more') {
        event.target.parentElement.parentElement.classList.toggle('showAll');
    }
}


/* LAYOUT HOME INDEX */
let topRatedList = document.querySelector('.topRatedList');
let mostPopularList = document.querySelector('.mostPopularList');
let neverRatedList = document.querySelector('.neverRatedList');

let divTopRated = document.querySelector('#topFlex');
let divMostPopular = document.querySelector('#mostPopularFlex');
let divNeverRated = document.querySelector('#neverRatedFlex');


let topRatedMovies = Array.from(topRatedList.children);
let mostPopularMovies = Array.from(mostPopularList.children);
let neverRatedMovies = Array.from(neverRatedList.children);

topRatedMovies[0].classList.add('topMovie');
mostPopularMovies[0].classList.add('topMovie');
neverRatedMovies[0].classList.add('topMovie');


topRatedMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
    divTopRated.appendChild(movie);
});

mostPopularMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
    divMostPopular.appendChild(movie);
});

neverRatedMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
    divNeverRated.appendChild(movie);
});

//Behöver helt klart kortas ner. 

