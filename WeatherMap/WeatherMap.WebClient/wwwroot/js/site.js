const apiUrl = 'http://localhost:5273/api/WeatherForecast/weather';
let todos = [];

function getItems() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayWeather(data) {
    const tBody = document.getElementById('weatherResult');
    tBody.innerHTML = data;       
}
function queryWeather() { 
    const weatherResult = document.getElementById('weatherResult');
    weatherResult.innerHTML = '';
    
    const searchParams = {
        country: document.getElementById('country').value.trim(),
        city: document.getElementById('city').value.trim()
    }; 
    fetch(`${apiUrl}/${searchParams.country}/${searchParams.city}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'APIKey': 'D0CAEF3B-6C13-4075-C0DD-1A2DE009EA46'
        }
    })
        .then(response => response.json())
        .then(data => {
            console.log(data.json());
            _displayWeather(data.json())
        })
        .catch(error => {
            console.log(JSON.stringify(error));
            weatherResult.innerHTML = error;
        });
       
    return false;
}