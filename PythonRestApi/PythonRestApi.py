import json
import os
import re
from fastapi import FastAPI
from pydantic import BaseModel

from leptonai.client import Client

app = FastAPI()

class Article(BaseModel):
    url: str

@app.post("/summarize")
def summarize_article(article: Article):
    # ����lepton API
    api_token = os.environ.get('LEPTON_API_TOKEN')
    client = Client("vqgajp1f", "my-search", token=api_token)

    result = client.query(
      query=article.url,
      search_uuid=article.url
    )
    
    result = result.decode('utf-8')

    llm_response_start = "__LLM_RESPONSE__"
    related_questions_start = "__RELATED_QUESTIONS__"
    
    index1 = result.find(llm_response_start) + len(llm_response_start)
    index2 = result.find(related_questions_start)
    
    llm_response = result[index1:index2].strip()

    # ȥ�����з���[citation: ]����
    summary = re.sub(r'\[citation:\d+\]', '', llm_response)
    summary = summary.replace('\n', ' ').strip()
    
    #print(summary)
    #print()
    
    related_questions = result[index2 + len(related_questions_start):].strip()
    #print(related_questions)
    #print()

    # ����__RELATED_QUESTIONS__ΪJSON����
    # related_questions = json.loads(related_questions)

    # summary = f"Summary of article at"  
    return {"Summary": summary, "related_questions": related_questions}


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
