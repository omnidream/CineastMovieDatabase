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
let myMovieList = document.querySelectorAll('.topList');
myMovieList.forEach(list => {
    list.firstElementChild.classList.add('topMovie');
})
