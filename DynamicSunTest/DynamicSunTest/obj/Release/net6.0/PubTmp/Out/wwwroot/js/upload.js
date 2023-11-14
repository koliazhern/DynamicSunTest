const form = document.forms.namedItem("fileinfo");
const input = document.querySelector(".inputFile");

form.addEventListener(
    "submit",
    (event) => {
        const output = document.querySelector("#output");
        const formData = new FormData(form);

        formData.append("CustomField", "This is some extra data");

        const request = new XMLHttpRequest();
        request.open("POST", "/WeatherUpload/Upload", true);
        request.onload = (progress) => {
            output.innerHTML =
                request.status === 200
                    ? "Uploaded!"
                    : `Error ${request.status} occurred when trying to upload your file.<br />`;
        };

        request.send(formData);
        event.preventDefault();
    },
    false,
);

input.addEventListener(
    "change",
    (event) => {
        console.log(input.files);
        const lbl = document.getElementById("label").querySelector("span");

        lbl.innerText = "Files were added";
    }
    );