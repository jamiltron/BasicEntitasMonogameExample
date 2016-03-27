using Entitas.CodeGenerator;
using System;
using System.IO;
using System.Reflection;

namespace BasicEntitasMonogameExample {
  public static class CodeGenerationEngine {
    public static void Generate() {
      Console.WriteLine("Generating code...");
      var generatedDirectory = GetProjectDirectory() + "/Generated/";

      var codeGenerators = new ICodeGenerator[] {
        new ComponentExtensionsGenerator(),
        new ComponentIndicesGenerator(),
        new PoolAttributesGenerator(),
        new PoolsGenerator()
      };

      var assembly = Assembly.GetAssembly(typeof(CodeTemplates));
      var provider = new TypeReflectionProvider(assembly.GetTypes(), new string[0]);
      var files = CodeGenerator.Generate(provider, generatedDirectory, codeGenerators);

      foreach (var file in files) {
        Console.WriteLine("Generated: " + generatedDirectory + file.fileName);
      }
      Console.WriteLine("...done generating code!");
    }

    static string GetProjectDirectory() {
      var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
      const string projectName = "BasicEntitasMonogameExample";
      while (dirInfo.Name != projectName) {
        dirInfo = dirInfo.Parent;
      }
      return dirInfo.FullName;
    }
  }
}

