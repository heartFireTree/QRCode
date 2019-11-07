this.$ajax.post("/webapi/",{paramer},{responseType:"blob"}).then(res => {
      const blob = new Blob([res], {
          type: "application/octet-stream;charset=utf-8"
      });
       if ("download" in document.createElement("a")) {
          // 非IE下载
          const elink = document.createElement("a");
          elink.download = "下载文件名.png"
          elink.style.display = "none";
          elink.href = URL.createObjectURL(blob);
          document.body.appendChild(elink);
          elink.click();
          URL.revokeObjectURL(elink.href); // 释放URL 对象
          document.body.removeChild(elink);
      } else {
          // IE10+下载
          navigator.msSaveBlob(blob, fileName);
      }
})
