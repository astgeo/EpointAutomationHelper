
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"GenActionYml With",
            content:"GenActionYml With",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/With',
            title:"GenActionYml.With",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"OpenFileOperation",
            content:"OpenFileOperation",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/OpenFileOperation',
            title:"OpenFileOperation",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"GenActionYml StepB",
            content:"GenActionYml StepB",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/StepB',
            title:"GenActionYml.StepB",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"DirHelper",
            content:"DirHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/DirHelper',
            title:"DirHelper",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"IDNumGen AreaCodeXml",
            content:"IDNumGen AreaCodeXml",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/AreaCodeXml',
            title:"IDNumGen.AreaCodeXml",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"PopupWatcherLibrary",
            content:"PopupWatcherLibrary",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/PopupWatcherLibrary',
            title:"PopupWatcherLibrary",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"GenActionYml",
            content:"GenActionYml",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/GenActionYml',
            title:"GenActionYml",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"GenActionYml Push",
            content:"GenActionYml Push",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/Push',
            title:"GenActionYml.Push",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"JsonHelper",
            content:"JsonHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/JsonHelper',
            title:"JsonHelper",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"GenActionYml ActionYML",
            content:"GenActionYml ActionYML",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/ActionYML',
            title:"GenActionYml.ActionYML",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"XmlHelper",
            content:"XmlHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/XmlHelper',
            title:"XmlHelper",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"GenActionYml Build",
            content:"GenActionYml Build",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/Build',
            title:"GenActionYml.Build",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"SqlOperationHelper",
            content:"SqlOperationHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/SqlOperationHelper',
            title:"SqlOperationHelper",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"Constant",
            content:"Constant",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/Constant',
            title:"Constant",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"GitOperation",
            content:"GitOperation",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/GitOperation',
            title:"GitOperation",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"HashHelper",
            content:"HashHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/HashHelper',
            title:"HashHelper",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"GenActionYml StepA",
            content:"GenActionYml StepA",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/StepA',
            title:"GenActionYml.StepA",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"HotKey",
            content:"HotKey",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/HotKey',
            title:"HotKey",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"ImageHelper",
            content:"ImageHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/ImageHelper',
            title:"ImageHelper",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"ExcelHelper",
            content:"ExcelHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/ExcelHelper',
            title:"ExcelHelper",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"AccessHelper",
            content:"AccessHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/AccessHelper',
            title:"AccessHelper",
            description:""
        }
    );
    a(
        {
            id:21,
            title:"CmdHelper",
            content:"CmdHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/CmdHelper',
            title:"CmdHelper",
            description:""
        }
    );
    a(
        {
            id:22,
            title:"FakeDataHelper",
            content:"FakeDataHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/FakeDataHelper',
            title:"FakeDataHelper",
            description:""
        }
    );
    a(
        {
            id:23,
            title:"ValidateHelper",
            content:"ValidateHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/ValidateHelper',
            title:"ValidateHelper",
            description:""
        }
    );
    a(
        {
            id:24,
            title:"Softwares",
            content:"Softwares",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/Softwares',
            title:"Softwares",
            description:""
        }
    );
    a(
        {
            id:25,
            title:"CaKeyHelper",
            content:"CaKeyHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/CaKeyHelper',
            title:"CaKeyHelper",
            description:""
        }
    );
    a(
        {
            id:26,
            title:"FileIOHelper",
            content:"FileIOHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/FileIOHelper',
            title:"FileIOHelper",
            description:""
        }
    );
    a(
        {
            id:27,
            title:"FileDialogHelper",
            content:"FileDialogHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/FileDialogHelper',
            title:"FileDialogHelper",
            description:""
        }
    );
    a(
        {
            id:28,
            title:"GenActionYml On",
            content:"GenActionYml On",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/On',
            title:"GenActionYml.On",
            description:""
        }
    );
    a(
        {
            id:29,
            title:"GenActionYml Jobs",
            content:"GenActionYml Jobs",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/Jobs',
            title:"GenActionYml.Jobs",
            description:""
        }
    );
    a(
        {
            id:30,
            title:"RegistryHelper",
            content:"RegistryHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/RegistryHelper',
            title:"RegistryHelper",
            description:""
        }
    );
    a(
        {
            id:31,
            title:"GenActionYml FatherStep",
            content:"GenActionYml FatherStep",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/FatherStep',
            title:"GenActionYml.FatherStep",
            description:""
        }
    );
    a(
        {
            id:32,
            title:"FtpHelper",
            content:"FtpHelper",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/FtpHelper',
            title:"FtpHelper",
            description:""
        }
    );
    a(
        {
            id:33,
            title:"IDNumGen",
            content:"IDNumGen",
            description:'',
            tags:''
        },
        {
            url:'/EpointAutomationHelper/api/EpointAutomationHelper/IDNumGen',
            title:"IDNumGen",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
