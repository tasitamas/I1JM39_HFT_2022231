let games = [];
let connection = null;

let gameIdToUpdate = -1;

setupSignalR();

getdata();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23247/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GameCreated", (user, message) => {
        getdata();
    });
    connection.on("GameDeleted", (user, message) => {
        getdata();
    });
    connection.on("GameUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
await fetch('http://localhost:23247/game')
    .then(x => x.json())
    .then(y => {
        games = y;
        //console.log(games);
        display()
    });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    games.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.gameId + "</td><td>" +
            t.gameName + "</td><td>" +
            t.price + "</td><td>" +
            t.release + "</td><td>" +
            t.rating + "</td><td>" +
            `<button type="button" onclick="remove(${t.gameId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.gameId})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}

function showupdate(id) {
    document.getElementById('gameNameToUpdate').value = games.find(t => t['gameId'] == id)['gameName']
    document.getElementById('gamePriceToUpdate').value = games.find(t => t['gameId'] == id)['price']
    document.getElementById('gameReleaseToUpdate').value = games.find(t => t['gameId'] == id)['release']
    document.getElementById('gameRatingToUpdate').value = games.find(t => t['gameId'] == id)['rating']
    document.getElementById('formdiv').style.display = 'none';
    document.getElementById('updateformdiv').style.display = 'flex';
    gameIdToUpdate = id;
}
function update() {
    document.getElementById('formdiv').style.display = 'flex';
    document.getElementById('updateformdiv').style.display = 'none';

    let name = document.getElementById('gameNameToUpdate').value;
    let price = document.getElementById('gamePriceToUpdate').value;
    let release = document.getElementById('gameReleaseToUpdate').value;
    let rating = document.getElementById('gameRatingToUpdate').value;

    fetch('http://localhost:23247/game', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { gameId:gameIdToUpdate, gameName: name, price: price, release: release, rating: rating }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:23247/game/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}
function create() {
    let name = document.getElementById('gameName').value;
    let price = document.getElementById('gamePrice').value;
    let release = document.getElementById('gameRelease').value;
    let rating = document.getElementById('gameRating').value;

    fetch('http://localhost:23247/game', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { gameName: name, price: price, release: release, rating:rating }
        ),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); }); 
}



