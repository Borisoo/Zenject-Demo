
namespace Asteroids
{
      public interface IRequireDependency<T>
      {
         T DependencyInterface { get; set; }
      }
}