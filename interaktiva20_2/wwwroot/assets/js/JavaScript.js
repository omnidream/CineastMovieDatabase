/*LIKES AND DISLIKES*/
let myLikes = 0;
let cmdbUrl = 'https://cmdbapi.kaffekod.se/api/';
document.querySelector(".likeBtn").addEventListener("click", like, false)

async function like() {
    let myImdbId = this.id;
    let myButton = this;
    myButton.disabled = true;

    await fetch(cmdbUrl + myImdbId + '/like').then((response) =>
    {
        if (response.ok) {
            myButton.disabled = false;
            return response.json();
        }
        else
            throw new Error('Something went wrong when calling CmdbAPI, sorry.');
    })
        .then(data => (myLikes = data.numberOfLikes))
        .then(updateNumberOfLikes)
        .catch((error) => {
            console.log(error)
        });
}

function updateNumberOfLikes() {
    document.querySelector(".snippety").innerHTML = "Likes: " + myLikes;
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


