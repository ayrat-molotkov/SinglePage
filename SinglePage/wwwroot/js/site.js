window.addEventListener('DOMContentLoaded', function () {
    'use strict';

    function addNewVirtualServer() {
        return $.ajax('/Home/AddVirtualServer', {
            type: "POST",
            contentType: 'application/json; charset=utf-8'
        });
    };    

    $('#add-button').on("click", function () {
        addNewVirtualServer()
            .done(function (data) {
                if (data.status == "success") {
                    let serverTr = document.createElement("tr");

                    serverTr.appendChild(createTh(data.newServer.virtualServerId));
                    serverTr.appendChild(createTh(getDateTimeFormat(data.newServer.createDateTime)));
                    serverTr.appendChild(createTh(data.newServer.removeDateTime !== null ? getDateTimeFormat(data.newServer.removeDateTime) : ""));

                    let removeCheckbox = document.createElement("input");
                    removeCheckbox.setAttribute('type', 'checkbox')
                    removeCheckbox.setAttribute('class', 'deleted-checkbox')
                    let checkboxTh = document.createElement("th");
                    checkboxTh.appendChild(removeCheckbox);

                    serverTr.appendChild(checkboxTh);

                    $('#serversTable')[0].getElementsByTagName('tbody')[0].appendChild(serverTr);

                    updateCurrentDateAndTotalUsageTime(data.nowTime, data.calculationTime);
                    listenCheckboxChange()
                } else {
                    alert('Server error')
                }
                
            })        
    });

    function getDateTimeFormat(dateStr) {
        var date = new Date(dateStr);
        var M = shortDateFormat((date.getMonth() + 1).toString()),
            d = shortDateFormat((date.getDate()).toString()),
            h = shortDateFormat((date.getHours()).toString()),
            m = shortDateFormat((date.getMinutes()).toString()),
            s = shortDateFormat((date.getSeconds()).toString())        

        return [date.getFullYear().toString(), M, d].join('.') + ' ' + [h, m, s].join(':')
    }

    function shortDateFormat(date) {
        if (date.length < 2) {
            return '0' + date;
        }

        return date;
    }

    function createTh(data) {
        let th = document.createElement("th");
        th.innerHTML = data;
        return th;
    }

    function deleteServers(ids) {
        return $.ajax('/Home/DeleteServers', {
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(ids)
        });
    };

    $('#delete-button').on("click", function () {
        var ids = []
        $('.deleted-checkbox:checked').each(function () {
            var parent = $(this).closest('.virtual-server-item')
            var id = parseInt(parent.attr('data-id'))
            ids.push(id);
        })
        deleteServers(ids)
            .done(function (data) {
                if (data.status == "success") {
                    $('.virtual-server-item').each(function () {

                        var id = parseInt($(this).attr('data-id'))
                        var server = filterItems(data.allServers, id)[0]

                        this.children[2].innerHTML = server.removeDateTime !== null ? getDateTimeFormat(server.removeDateTime) : "";
                        this.children[3].children[0].checked = false
                    })

                    updateCurrentDateAndTotalUsageTime(data.nowTime, data.calculationTime);

                    listenCheckboxChange();
                } else {
                    alert('Server error')
                }

            })
    });

    const filterItems = (arr, query) => {
        return arr.filter(el => el.virtualServerId == query)
    }

    function calculationWorkServersRequest() {
        return $.ajax('/Home/CalculationWorkServers', {
            type: "POST",
            contentType: 'application/json; charset=utf-8'
        });
    };
    

    function updateCurrentDateAndTotalUsageTime(currentDate, totalUsageTime) {
        $('#current-date')[0].innerHTML = getDateTimeFormat(currentDate)
        $('#total-usage-time')[0].innerHTML = totalUsageTime.substring(0, totalUsageTime.indexOf('.'))
    }

    function calculationWorkServers() {
        calculationWorkServersRequest()
            .done(function (data) {
                if (data.status == "success") {
                    updateCurrentDateAndTotalUsageTime(data.nowTime, data.calculationTime);
                } else {
                    alert('Server error')
                }
            })
    }

    setInterval(function () {
        calculationWorkServers();
    }, 60000);

    function listenCheckboxChange() {
        $('.deleted-checkbox').on("change", function () {
            calculationWorkServers()
        });
    }

    listenCheckboxChange();
});