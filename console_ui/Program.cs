using console_ui;
using data;
using data.DAO;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {

    }
}
//void getGroupsFromDB(IGroupRepository repository)
//{
//    foreach (var item in repository.getAllGroups())
//    {
//        Console.WriteLine($"{item.Id} {item.GroupName}");
//    }
//}

//IServiceCollection serviceCollection = new ServiceCollection();
//serviceCollection
//    .AddDbContext<RemoteDatabaseContext>()
//    .AddSingleton<IGroupRepository, SQLGroupRepository>()
//    .AddSingleton<IGroupUseCase, GroupService>()
//    .AddSingleton<GroupUI>();

//var serviceProvider = serviceCollection.BuildServiceProvider();
//var groupUI = serviceProvider.GetService<GroupUI>();

//groupUI?.addGroup(); //вызываем метод для создания новой группы и добавления ее в БД
////затем выводим все с помощью метода getGroupsFromDB
////жалуется на id




