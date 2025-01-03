﻿const initJqGrid = () => {
    $("#fileGrid").jqGrid({
        url: '/Book/GetFileBooks', // URL updated to match subfolder structure
        datatype: 'json',
        mtype: 'GET',
        colNames: ['ID', 'File Name', 'File Type', 'File Extension', 'Description', 'Uploaded By', 'Path', 'Uploaded Date'],
        colModel: [
            { name: 'Id', index: 'Id', width: 50, align: 'center' },
            { name: 'Name', index: 'Name', width: 200 },
            { name: 'Type', index: 'Type', width: 100 },
            { name: 'Extension', index: 'Extension', width: 80 },
            { name: 'Description', index: 'Description', width: 150 },
            { name: 'UploadBy', index: 'UploadBy', width: 100 },
            { name: 'Path', index: 'Path', width: 150 },
            { name: 'UploadedDate', index: 'UploadedDate', width: 100, align: 'center' }
        ],
        pager: '#filePager',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'Id',
        viewrecords: true,
        sortorder: "asc",
        gridview: true,
        caption: "File Books"
    });
};

$(document).ready(() => {
    initJqGrid();
});
