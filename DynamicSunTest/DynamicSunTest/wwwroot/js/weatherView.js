console.log("Hi");

const buttonsWrapper = document.querySelector(".button-wrapper");
const CreateButton = (year) => {
    const button = document.createElement("a");
    button.href = `/WeatherView/WeatherByYear?year=${year}`;
    button.classList.add("buttonYears");
    button.innerText = year;
    return button;
}

fetch("/WeatherView/GetYears")
    .then((response) => response.json())
    .then(years => {
        console.log(years);
        years.forEach(year => {
            buttonsWrapper.appendChild(CreateButton(year));
        });
    }
    );