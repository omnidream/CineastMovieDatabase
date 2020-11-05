/*READ MORE PLOT*/
let readMoreText = document.querySelectorAll('.moviePlot');
let link = document.querySelector('.more');
let numberOfShownChars = 130;

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
//TODO: PRIO 2 Korta ner Layout Home Index
let myMovieList = document.querySelector('.topList');
//let mostPopularList = document.querySelector('.mostPopularList');
//let neverRatedList = document.querySelector('.neverRatedList');

//let divTopRated = document.querySelector('#topFlex');
//let divMostPopular = document.querySelector('#mostPopularFlex');
//let divNeverRated = document.querySelector('#neverRatedFlex');

let topMovies = Array.from(myMovieList.children);
//let mostPopularMovies = Array.from(mostPopularList.children);
//let neverRatedMovies = Array.from(neverRatedList.children);

topMovies[0].classList.add('topMovie');
//mostPopularMovies[0].classList.add('topMovie');
//neverRatedMovies[0].classList.add('topMovie');


//topRatedMovies.slice(1).forEach(movie => {
//    movie.classList.add('movieList');
//    divTopRated.appendChild(movie);
//});

//mostPopularMovies.slice(1).forEach(movie => {
//    movie.classList.add('movieList');
//    divMostPopular.appendChild(movie);
//});

//neverRatedMovies.slice(1).forEach(movie => {
//    movie.classList.add('movieList');
//    divNeverRated.appendChild(movie);
//});