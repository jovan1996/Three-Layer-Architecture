using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.DataLayer;
using biznis.BussinessLayer.DTO;

namespace biznis.BussinessLayer.Operations
{
    public class PostCriteria : SelectCriteria
    {
        public string Name { get; set; }
    }

   
    public class OpPostBase : Operation
    {
        public PostDTO DTO { get; set; }

        public PostCriteria Criteria = new PostCriteria();

        public override OperationResult Execute(skiiiEntities entities)
        {
            IQueryable<Post> iqPost = entities.Posts;

            if (Criteria.Id != 0)
            {
                iqPost = iqPost.Where(r => r.idPost == Criteria.Id);
            }
            if (!String.IsNullOrEmpty(Criteria.Uuid))
            {
                iqPost = iqPost.Where(r => r.idUser == Criteria.Uuid);
            }
            IEnumerable<PostDTO> iePosts =
          (from p in iqPost
           orderby p.idPost descending
           select new PostDTO
           {
               Id = p.idPost,
              CategoryName=p.Category.name,
               heading = p.heading,
               paragraph = p.paragraph,
               publish = p.publish,
               src=p.Image.src,
               username = p.AspNetUser.UserName,
               ImageAbout=p.ImageAbout,
               paragraph2=p.paragraph2
           }).Take(3);

            var e = iqPost.Count();


            OperationResult result = new OperationResult
            {
                Items = iePosts.ToArray(),
                Status = true,
                Broj = e
            };

            return result;
        }
    }


    public class OpPostCategoryFilter : OpPostBase
    {
        //public PostDTO post=new PostDTO();
        public SelectCriteria Criteria = new SelectCriteria();


        public override OperationResult Execute(skiiiEntities entities)
        {
            IQueryable<Post> iePosts = entities.Posts;

            iePosts = iePosts.Where(r => r.idCategory == Criteria.Id);

            IEnumerable<PostDTO> iePostDTO = (from p in iePosts
                                              orderby p.idPost descending
                                              select new PostDTO
                                              {
                                                  Id = p.idPost,
                                                  CategoryName = p.Category.name,
                                                  heading = p.heading,
                                                  paragraph = p.paragraph,
                                                  publish = p.publish,
                                                  src = p.Image.src,                                                
                                                  username = p.AspNetUser.UserName
                                              }).Take(3);
            OperationResult result = new OperationResult();
            result.Items = iePostDTO.ToArray();
            result.Status = true;

            return result;

        }
    }
    public class OpPostBasePaginationFilter : OpPostBase
    {
        public SelectCriteria Criteria = new SelectCriteria();

        public override OperationResult Execute(skiiiEntities entities)
        {
            IQueryable<Post> iePosts = entities.Posts;


            IEnumerable<PostDTO> iePostDTO = (from p in iePosts
                                              orderby p.idPost descending
                                              select new PostDTO
                                              {
                                                  Id = p.idPost,
                                                  CategoryName = p.Category.name,
                                                  heading = p.heading,
                                                  paragraph = p.paragraph,
                                                  publish = p.publish,
                                                  src = p.Image.src,
                                                  username = p.AspNetUser.UserName
                                              }).Skip(Criteria.SkipFilter * 3).Take(3);
            OperationResult result = new OperationResult();
            result.Items = iePostDTO.ToArray();
            result.Status = true;

            return result;


        }
    }
    public class OpPostCategoryPaginateFilter : OpPostBase
    {
        public SelectCriteria Criteria = new SelectCriteria();

        public override OperationResult Execute(skiiiEntities entities)
        {

            IQueryable<Post> iePosts = entities.Posts;

            iePosts = iePosts.Where(r => r.idCategory == Criteria.Id);

            IEnumerable<PostDTO> iePostDTO = (from p in iePosts
                                              orderby p.idPost descending
                                              where p.idCategory == Criteria.Id
                                              select new PostDTO
                                              {
                                                  Id = p.idPost,
                                                  CategoryName = p.Category.name,
                                                  heading = p.heading,
                                                  paragraph = p.paragraph,
                                                  publish = p.publish,
                                                  src = p.Image.src,
                                                  username = p.AspNetUser.UserName
                                              }).Skip(Criteria.SkipFilter * 3).Take(3);
            OperationResult result = new OperationResult();
            result.Items = iePostDTO.ToArray();
            result.Status = true;

            return result;

        }
    }
    public class OpPostDelete : OpPostBase
    {

        public int idzabriranje { get; set; }


        public override OperationResult Execute(skiiiEntities entities)
        {


            Post postzabrisanje = entities.Posts.Where(p => p.idPost == idzabriranje).FirstOrDefault();

            if (postzabrisanje != null)
            {
                entities.Posts.Remove(postzabrisanje);
                entities.SaveChanges();
                return base.Execute(entities);
            }
            else
            {
                OperationResult result = new OperationResult();
                result.Status = false;
                result.Message = "Doslo je do greske, post ne postoji u bazi.";
                return result;
            }


        }
    }

    public class OpPostInsert : OpPostBase
    {
        public override OperationResult Execute(skiiiEntities entities)
        {
            var slika = new Image();
            slika.src = this.DTO.src;
            
            Post post = new Post();
            post.Image = slika;

            post.heading = DTO.heading;
            post.idCategory = 4;
            post.idUser = DTO.Uuid;
            post.ImageAbout = DTO.ImageAbout;
            post.paragraph = DTO.paragraph;
            post.paragraph2 = DTO.paragraph2;
            post.Image = slika;
            entities.Images.Add(slika);
            entities.Posts.Add(post);
            entities.SaveChanges();


            return base.Execute(entities);
        }
    }
}
