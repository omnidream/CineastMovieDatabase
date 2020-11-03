/*LIKES AND DISLIKES*/
let myImdbId;
let myCaller;
let myMovieObject;
let likeOrDislikeKey;
const cmdbUrl = 'https://cmdbapi.kaffekod.se/api/'; 
document.querySelector(".btnLike").addEventListener("click", likeDislike)


async function likeDislike() {
    myImdbId = this.dataset.imdbid;
    myCaller = this;
    likeOrDislikeKey = checkLikeDislike(myCaller)
    myCaller.disabled = true;
    myMovieObject = await GetCmdbApi();
    console.log(myMovieObject);
    updateNumberOfLikesDislikes()
    myCaller.disabled = false;
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
    let allElements = document.querySelectorAll('.likes')
    let updateThisElement = findElementToUpdate(allElements);
    updateThisElement.innerHTML = myMovieObject.numberOfLikes;
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


/* LAYOUT HOME INDEX */
let topRatedList = document.querySelector('.topRatedList');
let mostPopularList = document.querySelector('.mostPopularList');
let neverRatedList = document.querySelector('.neverRatedList');

let topRatedMovies = Array.from(topRatedList.children);
let mostPopularMovies = Array.from(mostPopularList.children);
let neverRatedMovies = Array.from(neverRatedList.children);

topRatedMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
});

mostPopularMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
});

neverRatedMovies.slice(1).forEach(movie => {
    movie.classList.add('movieList');
});

//Behöver helt klart kortas ner. 
