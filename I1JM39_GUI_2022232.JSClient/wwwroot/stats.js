let games = [];
let developers = [];
let characters = [];


//Default values
const defaultDescription = "Here you will see the description of the chosen stat...";
const defaultContent = "Here you will see the results...";

//Getting all data and loading the page in

defaultValuesToLoad();
getalldata();

async function getalldata() {
    await fetch('http://localhost:23247/game')
        .then(x => x.json())
        .then(y => {
            games = y;
            //console.log(games);
        });
    await fetch('http://localhost:23247/developer')
        .then(x => x.json())
        .then(y => {
            developers = y;
            //console.log(developers);
        });
    await fetch('http://localhost:23247/character')
        .then(x => x.json())
        .then(y => {
            characters = y;
            //console.log(characters); 
        });
}

//NON-CRUD Methods
async function getAvgRating() {
    await getalldata();
    const arr = [];
    for (var i = 0; i < games.length; i++) {
        arr[i] = games[i].rating;
    }
    const avg = arr.reduce((a, b) => a + b, 0) / arr.length;
    return avg.toFixed(2);
}
async function getSumOfGames() {
    await getalldata();
    //console.log(games.length);
    return games.length;
}
async function getOldestGame() {
    await getalldata();
    const oldest = games.sort((a, b) => a.release - b.release)[0];
    //console.log(oldest.gameName);
    return oldest.gameName;
}
async function getYoungestGame() {
    await getalldata();
    const youngest = games.sort((a, b) => b.release - a.release)[0];
    //console.log(youngest.gameName);
    return youngest.gameName;
}
async function getHighestPriceGame() {
    await getalldata();
    const mstExp = games.sort((a, b) => b.price - a.price)[0];
    //console.log(mstExp.gameName);
    return mstExp.gameName;
}
async function getFreeGames() {
    await getalldata();
    const freeGames = games.filter(game => game.price === 0).map(game => game.gameName);
    //console.log(freeGames);
    return freeGames.join(" - ");
}





//Display, loading, helping functions

function selectStat() {
    let statSelect = document.getElementById('statSelection');
    let selectedStat = statSelect.options[statSelect.selectedIndex].value;
    console.log(selectedStat);
    return selectedStat;
}
async function contentDecider() {
    //Description contents
    const avgRatingText = "This stat shows the average rating of the games.";
    const sumGamesText = "This stat shows the sum of the games.";
    const oldestGameText = "This stat shows the oldest game.";
    const youngestGameText = "This stat shows the youngest game.";
    const mstExpText = "This stat shows the most expensive game.";
    const freeGamesText = "This stat shows the list of free games.";



    if (selectStat() == "avgRating") {
        //console.log(`Average: ${await getAvgRating()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = avgRatingText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getAvgRating()} is average rating of the games.`;
    } else if (selectStat() == "sumOfGames") {
        //console.log(`Sum: ${await getSumOfGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = sumGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getSumOfGames()} games in total.`;
    } else if (selectStat() == "oldestGame") {
        //console.log(`Name: ${await getOldestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = oldestGameText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getOldestGame()} is the oldest game.`;
    } else if (selectStat() == "youngestGame") {
        //console.log(`Name: ${await getYoungestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = youngestGameText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getYoungestGame()} is the youngest game.`;
    } else if (selectStat() == "highestPrice") {
        //console.log(`Name: ${await getHighestPriceGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = mstExpText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getHighestPriceGame()} is the most expensive game.`;
    }
    else if (selectStat() == "freeGames") {
        //console.log(`List:\n ${await getFreeGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = freeGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getFreeGames()}`;
    }
}

function defaultValuesToLoad() {
    document.getElementById('statTable').rows[1].cells[0].innerHTML = defaultDescription;
    document.getElementById('statTable').rows[2].cells[0].innerHTML = defaultContent;
}



