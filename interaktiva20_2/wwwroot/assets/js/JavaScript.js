let myLikes = 0;
let cmdbUrl = 'https://localhost:5001/api/';
document.querySelector(".likeBtn").addEventListener("click", like)
let myImdbId = document.querySelector(".imdbidHidden").innerHTML;

async function like() {
    await fetch(cmdbUrl + 'movie/' + myImdbId + '/like')
        .then(response => response.json())
        .then(data => (myLikes = data.numberOfLikes))
        .then(updateNumberOfLikes);
}

function updateNumberOfLikes() {
    document.querySelector(".snippety").innerHTML = "Likes: " + myLikes;
}

