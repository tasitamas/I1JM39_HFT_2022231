let characters = [];
let connection = null;

let characterIdToUpdate = -1;

setupSignalR();

defaultValuesToLoad();

getdata();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23247/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CharacterCreated", (user, message) => {
        getdata();
    });
    connection.on("CharacterDeleted", (user, message) => {
        getdata();
    });
    connection.on("CharacterUpdated", (user, message) => {
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
    await fetch('http://localhost:23247/character')
        .then(x => x.json())
        .then(y => {
            characters = y;
            //console.log(developers);
            display()
        });
}

function showupdate(id) {
    document.getElementById('characterNameToUpdate').value = characters.find(t => t['characterId'] == id)['characterName']

    document.getElementById('characterPriorityToUpdate').value = priorityToNumber(id);

    document.getElementById('formdiv').style.display = 'none';
    document.getElementById('updateformdiv').style.display = 'flex';
    characterIdToUpdate = id;
}
function update() {
    document.getElementById('formdiv').style.display = 'flex';
    document.getElementById('updateformdiv').style.display = 'none';

    let name = document.getElementById('characterNameToUpdate').value;
    let priority = document.getElementById('characterPriorityToUpdate').value;

    fetch('http://localhost:23247/character', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { characterId: characterIdToUpdate, characterName: name, priority: priority }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    characters.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.characterId + "</td><td>" +
            t.characterName + "</td><td>" + priorityToString(t.priority) + "</td><td>" +
            `<button type="button" onclick="remove(${t.characterId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.characterId})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}
function remove(id) {
    fetch('http://localhost:23247/character/' + id, {
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
    let name = document.getElementById('characterName').value;
    let priority = Number(document.getElementById('characterPriority').value)

    fetch('http://localhost:23247/character', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { characterName: name, priority: priority }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function priorityToString(priority) {
    if (priority == 1) {
        return "Main Character";
    } else if (priority == 2) {
        return "Boss";
    } else {
        return "NPC";
    }
}
function priorityToNumber(id) {
    let input = characters.find(t => t['characterId'] == id);

    console.log(input);

    if (priorityToString(input.priority) == "Main Character") {
        return 1;
    } else if (priorityToString(input.priority) == "Boss") {
        return 2;
    } else {
        return 3;
    }
}
function defaultValuesToLoad() {
    document.getElementById('characterName').placeholder = "Enter your character name here...";
    document.getElementById('characterPriority').placeholder = "Priority";
}