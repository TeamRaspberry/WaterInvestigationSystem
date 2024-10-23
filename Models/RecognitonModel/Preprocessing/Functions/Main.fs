namespace Preprocessing
open MainApp

module FunctionsFS = 
    
    let testFunc string: string = 
        let testClass = TestClass()
        testClass.Name <- "r"
        testClass.ReturnName()