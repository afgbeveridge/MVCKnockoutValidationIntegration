var vmi = (function () {

    var logging = false, ignoreList = [], ignoredDirective = '_handling';

    function log(msg) {
        logging && console.log(msg);
    };

    function simpleProperty(obs, md, name) {
        log("Simple property: " + name);
        var cur = obs;
        if (ko.isObservable(cur) && md) {
            Object.keys(md).forEach(function (p) {
                if (ignoreList.indexOf(p) < 0) {
                    log("Extending property: " + name + ", directive = " + p + ", value = " + md[p]);
                    var directive = {};
                    directive[p] = md[p];
                    cur = cur.extend(directive);
                }
            });
        }
        return cur;
    };

    function isObservableArray(obj) {
        return ko.isObservable(obj) && obj.destroyAll !== undefined;
    };

    function interpret(model, metadata, validationTree) {
        if (model) {
            Object.keys(metadata).forEach(function (p) {
                log("Metadata property: " + p);
                var md = metadata[p];
                if (md && model.hasOwnProperty(p)) {
                    if (md[ignoredDirective] == 'simple') {
                        validationTree[p] = model[p];
                        model[p] = simpleProperty(model[p], md, p, validationTree);
                    }
                    else if (md[ignoredDirective] == 'complex') {
                        validationTree[p] = { };
                        interpret(ko.isObservable(model[p]) ? model[p]() : model[p], md, validationTree);
                    }
                    else if (md[ignoredDirective] == 'collection') {
                        var isObservable = isObservableArray(model[p]), arr = isObservable ? model[p]() : model[p];
                        if (Array.isArray(arr)) {
                            validationTree[p] = isObservable ? model[p] : arr;
                            arr.forEach(function (mem) {
                                interpret(mem, md, {});
                            });
                        }
                    }
                }
            });
        }
    };

    function apply(options) {
        ignoreList = options.ignoreList || [ignoredDirective];
        logging = typeof options.enableLogging === 'boolean' ? options.enableLogging : logging;
        var topLevelValidationTree = {};
        interpret(options.model, options.parsedMetadata, topLevelValidationTree);
        if (options.insertedValidatedObservableName)
            options.model[options.insertedValidatedObservableName] = ko.validatedObservable(topLevelValidationTree);
        return topLevelValidationTree;
    };

    function applyAsync(options) { 
        $.getJSON((options.endpoint || '/api/dynamicValidation/metadataFor') + '?typeName=' + options.typeName, null, function (response) {
            log(response);
            options.parsedMetadata = response;
            var validatedObject = apply(options);
            options.done && options.done(validatedObject, options.parsedMetadata);
        });
    };

    return {
        decorateAsync: applyAsync,
        decorate: apply,
        setIgnoredDirective: function (val) {
            ignoredDirective = val;
        }
    };

})();