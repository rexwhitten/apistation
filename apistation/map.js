// JavaScript source code

var Map = {};

function e(name, css_class) {
    var self = $('');

    self.attr('name', name);
    self.attr('id', name);
    self.addClass(css_class);

    return self;
}

function e_start(name) {
    var _e = e(name, "node-start");

    return _e;
}

function e_next(name) {
    var _e = e(name, "node-next");

    return _e;
}

function e_next_error(name) {
    var _e = e(name, "node-next-error");

    return _e;
}

function e_next_success(name) {
    var _e = e(name, "node-next-success");

    return _e;
}

function e_map(name, e_array) {
    var m = e(name, "e-map");

    for (var e_i in e_array) {
        m.append(e_array[e_i]);
    }

    return m;
}

Map.elements = {};
Map.elements.e = e;
Map.elements.start = e_start;
Map.elements.next = e_next;
Map.elements.next_success = e_next_success;
Map.elements.next_error = e_next_error;
Map.elements.map = e_map;

Map.Load = function (path) {

}



window.exports = {};
window.exports.Map = Map;