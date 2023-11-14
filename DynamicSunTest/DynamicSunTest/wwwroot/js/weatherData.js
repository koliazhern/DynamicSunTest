$(document).ready(function () {
    const table = $('#weatherTable').DataTable({
        paging: true,
        pageLength: 50,
    });

    // ��������� ���� �� ���������� URL
    const urlParams = new URLSearchParams(window.location.search);
    const year = urlParams.get('year');

    // ����� ������ ������� ��� ��������� ������
    fetch(`/WeatherView/GetWeatherData?year=${year}`)
        .then(response => response.json())
        .then(data => {
            table.clear();

            data.forEach(item => {
                table.row.add([
                    item.date,
                    item.time,
                    item.temperature,
                    item.humidity,
                    item.dewPoint,
                    item.atmospherePressure,
                    item.windDirection,
                    item.windSpeed,
                    item.cloudy,
                    item.cloudBase,
                    item.weatherConditions
                    // �������� ������ ��������, ���� ����������
                ]);
            });

            table.draw();
        });

});