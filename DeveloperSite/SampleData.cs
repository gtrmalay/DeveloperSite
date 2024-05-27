using DeveloperSite.Models;
using System.Numerics;

namespace DeveloperSite
{
    public class SampleData
    {
        public static void Initialize(MobileContext context, IWebHostEnvironment env)
        {
            if (!context.Games.Any())
            {
                context.Games.AddRange(
                    new Game
                    {
                        Game_name = "Miner",
                        Game_description = "\"Miner Clicker\" - это захватывающая игра в жанре кликер, где игрок берет на себя роль горняка, стремящегося к богатству и процветанию. В этой игре игрок управляет своим собственным шахтерским предприятием, где основным способом взаимодействия является клик мыши.",
                        Genre = "Кликер",
                        Publicate_date = new System.DateTime(2024, 5, 1),
                        Link_for_download = "ссылка на скачивание",
                        Img = "Miner.jpg"
                    },

                    new Game
                    {
                        Game_name = "TurboRoad",
                        Game_description = "\"TurboRoad\" — это захватывающий аркадный автомобильный симулятор, который призван втянуть игроков в адреналин и скорость. В этой игре игрок берет на себя роль гонщика, управляющего своим мощным турбированным автомобилем по захватывающим трассам и соперничая с другими гонщиками.",
                        Genre = "Симулятор",
                        Publicate_date = new System.DateTime(2024, 2, 1),
                        Link_for_download = "ссылка на скачивание",
                        Img = "TurboRoad.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}