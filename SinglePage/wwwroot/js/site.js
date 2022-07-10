window.addEventListener('DOMContentLoaded', function () {
    'use strict';

    function addNewVirtualServer() {
        return $.ajax('/Home/AddVirtualServer', {
            type: "POST",
            contentType: 'application/json; charset=utf-8'
        });
    };

    function calculationWorkServers() {
        return $.ajax('/Home/CalculationWorkServers', {
            type: "POST",
            contentType: 'application/json; charset=utf-8'
        });
    };

    $('#add-button').on("click", function () {
        addNewVirtualServer()
            .done(function (data) {
                if (data.status == "success") {
                    var table = $('#serversTable')
                    let serverTr = document.createElement("tr");

                    serverTr.appendChild(createTh(data.newServer.virtualServerId));
                    serverTr.appendChild(createTh(getDateTimeFormat(data.newServer.createDateTime)));
                    serverTr.appendChild(createTh(data.newServer.removeDateTime !== null ? getDateTimeFormat(data.newServer.removeDateTime) : ""));

                    let removeCheckbox = '<input type="checkbox" class="deleted-checkbox" ' + 'data-id="' + data.newServer.virtualServerId + '" />'

                    serverTr.appendChild(createTh(removeCheckbox));

                    table[0].getElementsByTagName('tbody')[0].appendChild(serverTr);

                    $('#current-date')[0].innerHTML = getDateTimeFormat(data.nowTime)
                    $('#total-usage-time')[0].innerHTML = data.calculationTime
                } else {
                    alert('Server error')
                }
                
            })

        //calculationWorkServers()
        //    .done(function (data) {
        //        $('#current-date')[0].innerHTML = getDateTimeFormat(data.nowTime)
        //        $('#total-usage-time')[0].innerHTML = data.calculationTime
        //    })
    });

    function getDateTimeFormat(dateStr) {
        var date = new Date(dateStr);
        var M = date.getMonth()+1,
            d = date.getDate(),
            h = date.getHours(),
            m = date.getMinutes(),
            s = date.getSeconds()

        return [date.getFullYear().toString(), M, d].join('.') + ' ' + [h, m, s].join(':')
    }

    function createTh(data) {
        let th = document.createElement("th");
        th.innerHTML = data;
        return th;
    }    
});