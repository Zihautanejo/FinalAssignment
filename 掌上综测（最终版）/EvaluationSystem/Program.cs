using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using EvaluationSystem.DAO;

namespace todo_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //鎶奃bContext鍔犲叆鍒板鍣?
            builder.Services.AddDbContext<EvaDbContext>(opt => opt.UseMySQL(
                "Server=localhost;Database=EvaSystemDB;User=root;Password=3306"));

            // 娣诲姞 CORS 鏈嶅姟閰嶇疆
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin() // 鍏佽浠绘剰鏉ユ簮
                           .AllowAnyMethod() // 鍏佽浠绘剰 HTTP 鏂规硶
                           .AllowAnyHeader(); // 鍏佽浠绘剰 HTTP 澶撮儴
                });
            });

            var app = builder.Build();

            // 鍦?Configure 鏂规硶涓惎鐢?CORS
            app.UseCors("AllowOrigin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles(); //鍚敤缂虹渷闈欐€侀〉闈紙index.html鎴杋ndex.htm锛?
            app.UseStaticFiles(); //鍚敤闈欐€佹枃浠讹紙椤甸潰銆乯s銆佸浘鐗囩瓑鍚勭鍓嶇鏂囦欢锛?

            app.UseHttpsRedirection(); //鍚姩http鍒癶ttps鐨勯噸瀹氬悜


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}