$(document).ready(function () {

    /* DataTables start here. */

    const dataTable = $('#aracsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "order": [[6, "desc"]],
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Arac/GetAllArac/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#aracsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const aracListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            if (aracListDto.ResultStatus === 0) {
                                $.each(aracListDto.Aracs.$values,
                                    function (index, arac) {
                                        const newTableRow = dataTable.row.add([
                                            arac.Id,
                                            arac.LicensePlate,
                                            convertToShortDate(arac.EnterDate),
                                            arac.Subscriber ? "Evet" : "Hayır",
                                            arac.SubscriberRFID,
                                            arac.Note,
                                            `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${arac.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${arac.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${arac.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#aracsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${aracListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#aracsTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* Ajax GET / Getting the _CategoryAddPartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Arac/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _CategoryAddPartial as Modal Form ends here. */

        /* Ajax POST / Posting the FormData as CategoryAddDto starts from here. */

        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-arac-add');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const aracAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', aracAddAjaxModel.AracAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = dataTable.row.add([
                            aracAddAjaxModel.AracDto.Arac.Id,
                            aracAddAjaxModel.AracDto.Arac.LicensePlate,
                            convertToShortDate(aracAddAjaxModel.AracDto.Arac.EnterDate),
                            aracAddAjaxModel.AracDto.Arac.Subscriber ? "Evet" : "Hayır",
                            aracAddAjaxModel.AracDto.Arac.SubscriberRFID,
                            aracAddAjaxModel.AracDto.Arac.Note,
                            `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${aracAddAjaxModel.AracDto.Arac.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${aracAddAjaxModel.AracDto.Arac.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${aracAddAjaxModel.AracDto.Arac.Id}`);
                        dataTable.draw();
                        toastr.success(`${aracAddAjaxModel.AracDto.Message}`, 'Başarılı İşlem!');
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                });
            });
    });

    /* Ajax POST / Posting the FormData as CategoryAddDto ends here. */

    /* Ajax POST / Deleting a Category starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const aracName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${aracName} adlı kayıt silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet',
                cancelButtonText: 'Hayır'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { aracId: id },
                        url: '/Admin/Arac/Delete/',
                        success: function (data) {
                            const aracDto = jQuery.parseJSON(data);
                            if (aracDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${aracDto.Arac.LicensePlate} adlı kayıt başarıyla silinmiştir.`,
                                    'success'
                                );
                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${aracDto.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!")
                        }
                    });
                }
            });
        });

    /* Ajax GET / Getting the _CategoryUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Arac/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { aracId: id }).done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        /* Ajax POST / Updating a Category starts from here */

        placeHolderDiv.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();

                const form = $('#form-arac-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const aracUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(aracUpdateAjaxModel);
                    const newFormBody = $('.modal-body', aracUpdateAjaxModel.AracUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        const id = aracUpdateAjaxModel.AracDto.Arac.Id;
                        const tableRow = $(`[name="${id}"]`);
                        placeHolderDiv.find('.modal').modal('hide');
                        dataTable.row(tableRow).data([
                            aracUpdateAjaxModel.AracDto.Arac.Id,
                            aracUpdateAjaxModel.AracDto.Arac.LicensePlate,
                            convertToShortDate(aracUpdateAjaxModel.AracDto.Arac.EnterDate),
                            aracUpdateAjaxModel.AracDto.Arac.Subscriber ? "Evet" : "Hayır",
                            aracUpdateAjaxModel.AracDto.Arac.SubscriberRFID,
                            aracUpdateAjaxModel.AracDto.Arac.Note,
                            `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${aracUpdateAjaxModel
                                .AracDto.Arac.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${aracUpdateAjaxModel
                                .AracDto.Arac.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).remove().draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Ödeme alınacak miktar : ',
                            text: `${aracUpdateAjaxModel.AracDto.Arac.Price}`,
                        });
                        toastr.success(`${aracUpdateAjaxModel.AracDto.Message}`, "Başarılı İşlem!");
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            });

    });
});