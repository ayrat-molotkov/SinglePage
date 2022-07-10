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
                    var table = $('#serversTable')
                    let serverTr = document.createElement("tr");
                    serverTr.setAttribute('data-id', data.newServer.virtualServerId)
                    serverTr.setAttribute('class', 'virtual-server-item')

                    serverTr.appendChild(createTh(data.newServer.virtualServerId));
                    serverTr.appendChild(createTh(getDateTimeFormat(data.newServer.createDateTime)));
                    serverTr.appendChild(createTh(data.newServer.removeDateTime !== null ? getDateTimeFormat(data.newServer.removeDateTime) : ""));

                    let removeCheckbox = '<input type="checkbox" class="deleted-checkbox" ' + 'data-id="' + data.newServer.virtualServerId + '" />'

                    serverTr.appendChild(createTh(removeCheckbox));

                    table[0].getElementsByTagName('tbody')[0].appendChild(serverTr);

                    $('#current-date')[0].innerHTML = getDateTimeFormat(data.nowTime)
                    $('#total-usage-time')[0].innerHTML = data.calculationTime.substring(0, data.calculationTime.indexOf('.'))
                } else {
                    alert('Server error')
                }

            })
    });

    function getDateTimeFormat(dateStr) {
        var date = new Date(dateStr);
        var M = (date.getMonth() + 1).toString(),
            d = (date.getDate()).toString(),
            h = date.getHours(),
            m = date.getMinutes(),
            s = date.getSeconds()

        if (M.length < 2) {
            M = '0' + M;
        }
        if (d.length < 2) {
            d = '0' + d;
        }

        return [date.getFullYear().toString(), M, d].join('.') + ' ' + [h, m, s].join(':')
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

                    $('#current-date')[0].innerHTML = getDateTimeFormat(data.nowTime)
                    $('#total-usage-time')[0].innerHTML = data.calculationTime.substring(0, data.calculationTime.indexOf('.'))
                } else {
                    alert('Server error')
                }

            })
    });

    const filterItems = (arr, query) => {
        return arr.filter(el => el.virtualServerId == query)
    }
});