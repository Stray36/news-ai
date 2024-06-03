import os
from fastapi import FastAPI
from pydantic import BaseModel

from leptonai.client import Client

app = FastAPI()

class Article(BaseModel):
    url: str

@app.post("/summarize")
def summarize_article(article: Article):
    # µ÷ÓÃlepton API
    api_token = os.environ.get('LEPTON_API_TOKEN')
    client = Client("vqgajp1f", "my-search", token=api_token)

    result = client.query(
      query=article.url,
      search_uuid=article.url
    )
    # summary = f"Summary of article at"  
    # return {"summary": summary}
    result = str(result)
    return {"summary": result}

if __name__ == "__main__":
    import uvicorn
  # result = summarize_article(123)
  # print(result)
    uvicorn.run(app, host="0.0.0.0", port=8000)
