/// <reference path="../knockout.js" />

const mode = {
    create: 1,
    update: 2
};

var ItemController = function () {
    var self = this;
    const baseUrl = '/api/ItemAPI';
    self.NewItem = ko.observable(new ItemModel());
    self.CurrentItem = ko.observableArray([]);
    self.SelectedItem = ko.observableArray([]);
    self.IsUpdated = ko.observable(false);
    self.mode = ko.observable(mode.create);

    self.GetDatas = function () {
        ajax.get(baseUrl).then(function (result) {
            self.CurrentItem(result.map(item => new ItemModel(item)));
        });
    }

    self.GetDatas();

    self.AddItem = function () {
        var ItemData = ko.toJS(self.IsUpdated() ? self.SelectedItem : self.NewItem);
        switch (self.mode()) {
            case 1:
                ajax.post(baseUrl, JSON.stringify(ItemData))
                    .done(function (result) {
                        self.CurrentItem.push(new ItemModel(result));
                        self.CloseModel();
                        self.GetDatas();
                        $('#itemModal').modal('hide');
                    })
                break;
            case 2:
                ajax.put(baseUrl, JSON.stringify(ItemData))
                    .done(function (result) {
                        self.CurrentItem.replace(self.SelectedItem(), new ItemModel(result));
                        self.CloseModel();
                        self.GetDatas();
                        $('#itemModal').modal('hide');
                    })
        }
    }

    self.DeleteItem = function (model) {
        debugger
        ajax.delete(baseUrl + "?id=" + model.Id())
            .done(function (result) {
                self.CurrentItem.remove(result);
                self.GetDatas();
            })
            .fail((err) => {
                console.log(err);
            })
    }

    self.SelectItem = function (model) {
        self.SelectedItem(model);
        self.IsUpdated(true);
        self.mode(mode.update);
       
    }
    self.CloseModel = function () {
        self.ResetForm();
        self.GetDatas();
    }
    self.ResetForm = function () {
        self.CurrentItem(new ItemModel());
        self.SelectedItem(new ItemModel());
        self.IsUpdated(false);
    }

    
}
var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
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
            data: (data)
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
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
}