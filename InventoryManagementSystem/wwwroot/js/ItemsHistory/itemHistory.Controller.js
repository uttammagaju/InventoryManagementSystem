/// <reference path="itemhistory.model.js" />
/// <reference path="../knockout.js" />

var itemhistorycontroller = function () {
    var self = this;
    const baseUrl = "/api/ItemsInfoHistoryAPI";
    self.ItemHistoryList = ko.observableArray([]);

    self.searchArrayList = ko.observableArray([]);
    self.currentPage = ko.observable(1);
    self.itemsPerPage = ko.observable(10);
    self.totalPages = ko.computed(function () {
        return Math.ceil(self.ItemHistoryList().length / self.itemsPerPage());
    });

    self.pagedItems = ko.computed(function () {
        var startIndex = (self.currentPage() - 1) * self.itemsPerPage();
        return self.ItemHistoryList().slice(startIndex, startIndex + self.itemsPerPage());
    });

    // Navigation methods
    self.nextPage = function () {
        if (self.currentPage() < self.totalPages()) {
            self.currentPage(self.currentPage() + 1);
        }
    };

    self.previousPage = function () {
        if (self.currentPage() > 1) {
            self.currentPage(self.currentPage() - 1);
        }
    };

    self.goToPage = function (page) {
        self.currentPage(page);
    };

    self.GetDatas = async function () {
        try {
            let result = await ajax.get(baseUrl);
            self.ItemHistoryList(result.map(item => new itemhistorymodel(item)));
            self.currentPage(1);
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    self.GetDatas();
};

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
        });
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: JSON.stringify(data)
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: JSON.stringify(data)
        });
    },
    delete: function (url) {
        return $.ajax({
            method: "DELETE",
            url: url,
        });
    }
};
