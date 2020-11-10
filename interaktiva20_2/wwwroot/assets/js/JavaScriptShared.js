/*LIKES AND DISLIKES*/
let myImdbId;
let myCallerButton;
let myMovieObject;
let likeOrDislikeKey;
let myElementArray = [];
const cmdbUrl = 'https://cmdbapi.kaffekod.se/api/';

AddEventListenerToButtons(document.querySelectorAll('.btnLike'))
AddEventListenerToButtons(document.querySelectorAll('.btnDislike'))

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
    let updateTheseElements;
    
    updateTheseElements = findElementsToUpdate(document.querySelectorAll('.likes'));
    updateElementsInArray(updateTheseElements)

    updateTheseElements = findElementsToUpdate(document.querySelectorAll('.dislikes'));
    updateElementsInArray(updateTheseElements)
}

function findElementsToUpdate(myElements) {
    myElementArray = [];
    var i;
    for (i = 0; i < myElements.length; i++) {
        if (myElements[i].dataset.imdbid === myImdbId)
            myElementArray.push(myElements[i])
    }
    return myElementArray;
}

function updateElementsInArray(myArray) {
    myArray.forEach(element => {
        if (element.dataset.likeDislikeText === 'likes')
            element.innerHTML = 'Likes: '+ myMovieObject.numberOfLikes;
        else
            element.innerHTML = 'Dislikes: '+ myMovieObject.numberOfDislikes;
    })
}

/* STICKY SEARCH ON SCROLL */
window.onscroll = function () { addStickyClass() };

let mySearchBar = document.querySelector('.searchContainer-main');
let sticky = mySearchBar.offsetTop;

function addStickyClass() {
    if (window.pageYOffset > sticky) 
        mySearchBar.classList.add("sticky");
    else 
        mySearchBar.classList.remove("sticky");
}


//SEACH RESULT PAGE TO PREVENT OVERSTEPPING NUMBER OF SEARCHED PAGES
let firstPage = 1;

if (document.querySelector('.total-pages') != null)
{
    let lastPage = document.querySelector('.total-pages').innerHTML
    let currentPage = document.querySelector('#current-page').innerHTML
    let previousBtn = document.querySelector('.search-result-previous-btn')
    let nextBtn = document.querySelector('.search-result-next-btn')
    let firstBtn = document.querySelector('.search-result-first-btn')
    let lastBtn = document.querySelector('.search-result-last-btn')

    if (currentPage == firstPage) {
        previousBtn.disabled = true;
        firstBtn.disabled = true;
    }

    if (currentPage == lastPage) {
        nextBtn.disabled = true;
        lastBtn.disabled = true;
    }
}


//AUTOCMPLETE, very much inspired (but adapted) from https://codepen.io/logistus/pen/qJMOKZ
$(document).ready(function () {

    function highlight(word, query) {
        let check = new RegExp(query, "ig")
        return word.toString().replace(check, function (matchedText) {
            return "<u style='background-color: yellow'>" + matchedText + "</u>"
        })
    }

    $("#result-list").hide()
    $("#list").hide()

    $(".searchTerm").keyup(function () {
        let search = $(this).val()
        let results = ""
        if (search == "") {
            $("#result-list").hide()
            $(".search-input").removeClass("arrow").addClass("search")
        } else {
            $(".search-input").removeClass("search").addClass("arrow")
        }

        $.getJSON("https://www.omdbapi.com/?", { apikey: "fde91161", s: search }, function (data) {
            if (data.Search !== undefined) {
                $.each(data.Search, function (index, value) {
                    if (index < 3) {
                        $.getJSON("https://www.omdbapi.com/?", { apikey: "fde91161", i: value.imdbID }, function (movieData) {
                            if (movieData) {
                                results += '<a class="movie-link" href="/details?imdbid=' + movieData.imdbID + '")>'
                                results += '<div class="movie-auto-container">'
                                results += '<div><img src=' + movieData.Poster + ' style="width: 50px; height: auto;" /></div>'
                                results += '<div>'
                                results += '<div class="movie-title">' + highlight(movieData.Title, $(".searchTerm").val()) + ' (' + movieData.Year + ')</div>'
                                results += '</div>'
                                results += '</div>'
                                results += '</a>'
                                $("#results").html(results)
                            }
                        })
                    }
                });
                $("#result-list").show()
            }
        });
    });

    $("#searchAgain").click(function () {
        $("#search").show()
        $("#list").hide()
        $("#result-list").hide()
        $(".search-input").val("")
    });
});

